﻿using ConfigFile;
using EncodeData;
using log4net;
using log4net.Appender;
using log4net.Config;
using PDC_230;
using PDC230Lib;
using System;
using System.IO;
using System.IO.Ports;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Utils.Xml;

namespace MyNumberCard
{

    public partial class IssueForm : Form
    {

        private ILog logger;

        private XmlFile _SettingsFile;
        private XmlFile _NumberingMasterFile;
        private Settings _Settings = new Settings();
        private NumberingMaster _NumberingMaster = new NumberingMaster();

        private bool NumberingProc = true;  // 採番モードフラグ
        private bool IssueProc = false;     // 発行モードフラグ
        private bool CardReadProc = false;  // カード読取フラグ
        private bool CardWriteProc = false;  // 読込情報書込みフラグ

        //add 2024/12/11 str
        //private char PaddingChar;
        private int FirstPoint = 0;         //開始位置
        private string BranchOfficecheck;   //拠点コードチェック        
        private string PaddingcheckL;       //左詰めチェック
        private char PaddingCharL;          //　〃　0 or 空白
        private string PaddingcheckR;       //右詰めチェック
        private char PaddingCharR;          //　〃　0 or 空白
        //add 2024/12/11 end

        private string IniFilePath;
        private static int ComPortNum = 1;
        private static string PortName;

        public IssueForm()
        {
            InitializeComponent();

            string logFolder = Properties.Settings.Default.IssueLogPath;
            InitializeLog4Net(logFolder);
            logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            //保存場所取得
            Assembly myAssembly = Assembly.GetEntryAssembly();
            string path = myAssembly.Location;
            string[] sAray = path.Split('\\');
            sAray[sAray.Length - 1] = @"PDC-230.ini";
            IniFilePath = string.Join(@"\", sAray);

            //Portの読み込み
            if (File.Exists(IniFilePath))
            {
                //ポート番号取得
                ComPortNum = (int)ClsIniFileHandler.GetPrivateProfileInt("COM", "PORT", ComPortNum, IniFilePath);
                //ポート番号から名称を作成                                
                PortName = "COM" + ComPortNum.ToString();
            }

            //add 2024/12/11 str

            //PDC-230が接続されているポートを検索・比較・更新

            string[] port = GetPortNames();

            if (!(port == null))
            {
                if (!(port[0] == PortName))
                {
                    if (DialogResult.OK == MessageBox.Show("機器が接続されている通信ポートと異なる通信ポートが登録されています。\n変更しますか？", "通信ポート", MessageBoxButtons.OKCancel))
                    {
                        //ファイルの中身を書き換え
                        ComPortNum = int.Parse(port[0].Replace("COM", ""));
                        ClsIniFileHandler.WritePrivateProfileString("COM", "PORT", ComPortNum.ToString(), IniFilePath);
                        PortName = port[0];
                    }
                }
            }

            //add 2024/12/11 end

            InputModeChange();

            try
            {
                string confpath = Properties.Settings.Default.ConfigFilePath;
                _SettingsFile = new XmlFile(confpath, _Settings);
                _Settings = (Settings)_SettingsFile.Read();

                // 開始位置情報
                FirstPoint = int.Parse(_Settings.StartPosition.Value);      //add 2024/12/11

                // ヘッダー情報
                MunicipalCodeTextBox.MaxLength = (int)_Settings.MunicipalCode.Length;
                MunicipalCodeTextBox.Text = _Settings.MunicipalCode.Value;

                // 拠点コード情報                
                BranchOfficeTextBox.MaxLength = (int)_Settings.BranchOffice.Length;

                //add 2024/12/11 str
                //BranchOfficeTextBox.Text = _Settings.BranchOffice.Value;
                BranchOfficecheck = _Settings.BranchOffice.PaddingStatus.ToString();
                if (BranchOfficecheck == "ON")
                {
                    BranchOfficeTextBox.Text = _Settings.BranchOffice.Value;
                }
                else
                {
                    BranchOfficeTextBox.Text = "";
                }
                //add 2024/12/11 end

                // 印鑑登録番号情報
                NumberingTextBox.MaxLength = (int)_Settings.SealRegistration.Length;

                //add 2024/12/11 str
                // 左データ埋め                
                PaddingcheckL = _Settings.PaddingLeft.PaddingStatus.ToString();
                PaddingCharL = _Settings.PaddingLeft.PaddingCharacter;
                //add 2024/12/11 end

                // 右データ埋め
                //add 2024/12/11 str
                //PaddingChar = _Settings.Padding.PaddingCharacter;
                PaddingcheckR = _Settings.Padding.PaddingStatus.ToString();
                PaddingCharR = _Settings.Padding.PaddingCharacter;
                //add 2024/12/11 end

                // 設定ファイル取得
                string numpath = Properties.Settings.Default.NumberingMasterPath;
                _NumberingMasterFile = new XmlFile(numpath, _NumberingMaster);

                // ログ出力
                logger.Debug("ConfPath:" + confpath);
                logger.Debug("NumPath:" + numpath);

            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "ファイルオープンエラー");
                logger.Error(Properties.Resources.PossibleCauses);
                _inputmode = true;
                Application.Exit();
            }
            catch (Exception)
            {
                MessageBox.Show(Properties.Resources.NotFoundConfigFile + "\nシステム管理者に問い合わせてください。", "システムエラー");
                logger.Error(Properties.Resources.NotFoundConfigFile);
                _inputmode = true;
                Application.Exit();
            }
        }

        #region log4net

        private static void InitializeLog4Net(string logFolder)
        {

            XmlConfigurator.Configure(LogManager.GetRepository(), new FileInfo("log4Net.config"));

            log4net.Repository.ILoggerRepository[] repositories = LogManager.GetAllRepositories();
            foreach (var repository in repositories)
            {
                foreach (var appender in repository.GetAppenders())
                {
                    if (appender.Name == "InfoLogDailyAppender")
                    {
                        FileAppender fileAppender = appender as FileAppender;
                        fileAppender.File = logFolder + @"\info_" + DateTime.Now.ToString("yyyy_MM") + ".log";
                        fileAppender.ActivateOptions();
                    }
                }
            }
        }

        #endregion

        #region Status displaying (infomation)


        private void DisplayingIssueMessages(string data)
        {
            StatusLabel.Text = data + "を書込みました。";
            logger.Info("[発行] : " + data);
        }
        private void DisplayingUnissuedMessage(string data)
        {
            StatusLabel.Text = "発行処理を中断しました";
            logger.Info("[未発行] : " + data);
        }

        #endregion
        #region Status displaying (worning)
        private void DisplayingStatusMessages(string status)
        {
            string message;
            message = StatusCode.ToMessage(status);
            StatusLabel.Text = message;
            logger.Warn(message);
        }
        #endregion

        private void DisplayDescription(string str)
        {
            StatusLabel.Text = str;
            logger.Debug(str);
        }

        #region PDC230
        private string[] GetPortNames(string IniFile = "")
        {

            string[] port = new string[0];

            if (!(IniFile == ""))
            {
                //ファイル指定
                if (PortName != null)
                {
                    port = new string[1];
                    port[0] = PortName;
                }
            }
            else
            {
                //ポート検索
                port = PDC230.GePortNames();
            }

            if (0 == port.Length)
            {
                //未設定
                string message = Properties.Resources.PDC230Disconnected;
                MessageBox.Show(message, "接続未設定");
                logger.Error(message);
                port = null;
            }
            else if (1 < port.Length)
            {
                //複数端末接続
                string message = Properties.Resources.PDC230MultipleConnections;
                MessageBox.Show(message, "接続エラー");
                logger.Error(message);
                for (int i = 0; i < port.Length; i++)
                {
                    logger.Debug("GetPortNames:" + port[i]);
                }
            }
            else
            {
                logger.Debug("GetPortName:" + port[0]);
            }

            return port;

        }

        #endregion


        private void IssueWaitModeChange()
        {
            NumberingButton.Enabled = false;
            ManualInputButton.Enabled = false;
            CardReadButton.Enabled = false;
            IssueButton.Enabled = true;
            CancelButton.Enabled = true;

            NumberingTextBox.ReadOnly = true;
            _inputmode = false;
        }

        private bool _inputmode = true;

        private void InputModeChange()
        {
            NumberingButton.Enabled = true;
            ManualInputButton.Enabled = true;
            CardReadButton.Enabled = true;
            IssueButton.Enabled = false;
            CancelButton.Enabled = false;

            NumberingTextBox.ReadOnly = true;
            _inputmode = true;
        }
        private void IssueingModeChange()
        {
            NumberingButton.Enabled = false;
            ManualInputButton.Enabled = false;
            CardReadButton.Enabled = false;
            IssueButton.Enabled = false;
            CancelButton.Enabled = true;
            _inputmode = false;
        }
        private void WriteingModeChange()
        {
            NumberingButton.Enabled = false;
            ManualInputButton.Enabled = false;
            CardReadButton.Enabled = false;
            IssueButton.Enabled = false;
            CancelButton.Enabled = false;
            _inputmode = false;
        }
        private void ConfigRead()
        {
            _SettingsFile.Read();
            _NumberingMasterFile.Read();
        }
        private void ExitMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
        private void SettingMenuItem_Click(object sender, System.EventArgs e)
        {
            OwnSettingForm form = new OwnSettingForm();
            form.Show();
        }

        // 印鑑登録番号 採番ボタンクリック
        private void NumberingButton_Click(object sender, System.EventArgs e)
        {

            //add 2024/12/11 str            
            StatusPictureBox.Image = Properties.Resources.White;
            CardReadProc = false;
            //add 2024/12/11

            IssueWaitModeChange();
            try
            {
                _NumberingMaster = (NumberingMaster)_NumberingMasterFile.Read();
                ulong num = _NumberingMaster.Number;
                int len = (int)_Settings.SealRegistration.Length;
                if ((num).ToString().Length == len + 1)
                {                    
                    string msg = "印鑑登録番号が上限に達しました。\nシステム管理者に問い合わせてください。";
                    MessageBox.Show(msg, "採番エラー");
                    logger.Error(msg);
                    InputModeChange();
                }
                else
                {
                    _NumberingMaster.Number++;
                    _NumberingMasterFile.Write(_NumberingMaster);
                    NumberingTextBox.Text = num.ToString("D" + len.ToString());

                    DisplayDescription(NumberingTextBox.Text + "を採番しました。");
                    NumberingProc = true;
                }
            }
            //add 2024/12/11 str            
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "ファイルオープンエラー");
                logger.Error(Properties.Resources.PossibleCauses);
                InputModeChange();
            }
            //add 2024/12/11 end
            catch (Exception)
            {
                MessageBox.Show(Properties.Resources.NotFoundConfigFile + "\nシステム管理者に問い合わせてください。", "システムエラー");
                logger.Error(Properties.Resources.NotFoundConfigFile);
                _inputmode = true;
                Application.Exit();
            }
        }

        // 印鑑登録番号 入力ボタンクリック
        private void ManualInputButton_Click(object sender, System.EventArgs e)
        {
            //add 2024/12/11 str            
            StatusPictureBox.Image = Properties.Resources.White;
            CardReadProc = false;
            //add 2024/12/11

            IssueWaitModeChange();
            NumberingTextBox.Text = string.Empty;
            NumberingTextBox.ReadOnly = false;
                        
            DisplayDescription("印鑑登録番号を入力してください。");     
            NumberingProc = false;
            NumberingTextBox.Focus();

        }

        private string NumberingPadding()
        {
            int width = (int)_Settings.SealRegistration.Length;
            char padding = _Settings.SealRegistration.PaddingCharacter;
            return NumberingTextBox.Text.PadLeft(width, padding);
        }

        // 発行ボタンクリック
        private void IssueButton_Click(object sender, System.EventArgs e)
        {            
                if (DialogResult.OK == MessageBox.Show("カードを発行します。よろしいですか？", "カード発行", MessageBoxButtons.OKCancel))
                {

                StatusPictureBox.Image = Properties.Resources.Blue;
                IssueingModeChange();

                NumberingTextBox.Text = NumberingPadding();

                string[] port = GetPortNames(IniFilePath);

                CardReadProc = false;

                if (port == null)
                {
                    //未設定
                    StatusPictureBox.Image = Properties.Resources.White;
                    IssueWaitModeChange();
                    return;
                }
                else if (port.Length != 1)
                {
                    //複数台
                    if (Properties.Settings.Default.PortName != string.Empty)
                    {
                        StatusPictureBox.Image = Properties.Resources.White;
                        IssueWaitModeChange();
                        return;
                    }
                    port[0] = Properties.Settings.Default.PortName;
                }

                IssueProc = true;
                _canceled = false;
                CardReadProc = false;   //add 2024/12/11

                string data = string.Empty;

                PDC230 pdc230 = new PDC230(port[0], 9600, Parity.Even, 8, StopBits.One);

                //add 2024/12/11 str                

                int width;
                char padding;

                cardReaderInfo.Text = "";
                OldcardInfo.Text = "";

                //add 2024/12/11 end

                try
                {
                    pdc230.Open();
                    pdc230.Cancel();
                    pdc230.Insert();

                    DisplayDescription("発行処理を行います。カードを挿入してください。");

                    string stat;
                    while (true)
                    {
                        Thread.Sleep(200);
                        Application.DoEvents();
                        stat = pdc230.GetStatus();
                        if (stat != string.Empty)
                        {
                            break;
                        }
                        if (_canceled == true)
                        {
                            pdc230.Cancel();
                            break;
                        }
                    }
                    if (_canceled != true)
                    {
                        //add 2024/12/11 str                        
                        //事前にカード情報読込
                        pdc230.CardRead();
                        while (true)
                        {
                            Thread.Sleep(200);
                            Application.DoEvents();
                            data = pdc230.GetData();
                            if (data != string.Empty)
                            {
                                OldcardInfo.Text = data;
                                break;
                            }
                            stat = pdc230.GetStatus();
                            if (stat != string.Empty)
                            {
                                break;
                            }
                        }

                        if (stat != string.Empty)
                        {
                            pdc230.Eject();
                        }
                        else
                        { 
                            //add 2024/12/11 end

                            DisplayDescription("発行処理中です。");
                            WriteingModeChange();
                            StatusPictureBox.Image = Properties.Resources.Orange;

                            //add 2024/12/11 str

                            //pdc230.CardWrite(MunicipalCodeTextBox.Text + BranchOfficeTextBox.Text + NumberingTextBox.Text, PaddingChar);

                            //左詰め
                            if ((!(FirstPoint == 1) && PaddingcheckL == "Left"))
                            {
                                //「0 or 空白」で埋める
                                width = FirstPoint - 1;
                                padding = PaddingCharL;
                                cardReaderInfo.Text = cardReaderInfo.Text.PadLeft(width, padding);
                            }
                            else if ((!(FirstPoint == 1) && PaddingcheckL == "None"))
                            {
                                //前のカード情報を取得・埋め込み                            
                                cardReaderInfo.Text = OldcardInfo.Text.Substring(0, Math.Min(FirstPoint - 1, OldcardInfo.Text.Length));
                            }

                            //ヘッダー + 拠点 + 印鑑登録番号
                            cardReaderInfo.Text += MunicipalCodeTextBox.Text + BranchOfficeTextBox.Text + NumberingTextBox.Text;

                            //右詰め
                            if ((!(FirstPoint == 65) && PaddingcheckR == "Right"))
                            {
                                //「0 or 空白」で埋める
                                width = 69;
                                padding = PaddingCharR;
                                cardReaderInfo.Text = cardReaderInfo.Text.PadRight(width, padding);
                            }
                            else if ((!(FirstPoint == 65) && PaddingcheckR == "None"))
                            {
                                //前のカード情報を取得・埋め込み                            
                                cardReaderInfo.Text += OldcardInfo.Text.Substring(cardReaderInfo.Text.Length);
                            }

                            pdc230.CardWrite(cardReaderInfo.Text);

                            //add 2024/12/11 end

                            while (true)
                            {
                                Thread.Sleep(200);
                                Application.DoEvents();
                                data = pdc230.GetData();
                                if (data != string.Empty)
                                {
                                    break;
                                }
                                stat = pdc230.GetStatus();
                                if (stat != string.Empty)
                                {
                                    break;
                                }
                            }
                        }

                    }

                    pdc230.Close();
                    if (_canceled == true)
                    {
                        DisplayDescription("カード挿入待ちを中断しました。");
                        StatusPictureBox.Image = Properties.Resources.White;
                        IssueWaitModeChange();
                        _canceled = false;
                    }
                    else if (data != string.Empty)
                    {
                        DisplayingIssueMessages(data);
                        StatusPictureBox.Image = Properties.Resources.White;
                        InputModeChange();
                    }
                    else if (stat != string.Empty)
                    {
                        if (int.Parse(stat) >= 81)
                        {
                            StatusPictureBox.Image = Properties.Resources.Red;
                        }
                        else
                        {
                            StatusPictureBox.Image = Properties.Resources.Yellow;
                        }
                        DisplayingStatusMessages(stat);
                        IssueWaitModeChange();
                    }
                    IssueProc = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Properties.Resources.PDC230Error + "\n" + ex.Message, "システムエラー");
                    logger.Error(Properties.Resources.PDC230Error + ex.Message);
                    StatusPictureBox.Image = Properties.Resources.White;
                    IssueWaitModeChange();
                }
                finally
                {
                    pdc230.Close();
                    IssueProc = false;
                }

            }
        }

        //add 2024/12/11 str        
        // 印鑑登録番号 読込ボタンクリック
        private void CardReadButton_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("カード情報を読み込みますか？", "カード読取", MessageBoxButtons.OKCancel))
            {
                                
                StatusPictureBox.Image = Properties.Resources.Green;

                //カード情報読取                               

                IssueingModeChange();

                string[] port = GetPortNames(IniFilePath);

                if (port == null)
                {
                    //未設定
                    StatusPictureBox.Image = Properties.Resources.White;
                    InputModeChange();
                    return;
                }
                else if (port.Length != 1)
                {
                    //複数台
                    if (Properties.Settings.Default.PortName != string.Empty)
                    {
                        StatusPictureBox.Image = Properties.Resources.White;
                        InputModeChange();
                        return;
                    }
                    port[0] = Properties.Settings.Default.PortName;
                }

                IssueProc = true;
                _canceled = false;
                CardReadProc = true;
                CardWriteProc = true;

                string data = string.Empty;
                string stat = string.Empty;

                PDC230 pdc230 = new PDC230(port[0], 9600, Parity.Even, 8, StopBits.One);

                cardReaderInfo.Text = "";

                try
                {

                    pdc230.Open();
                    pdc230.Cancel();
                    pdc230.Insert();

                    DisplayDescription("読取元カードを挿入してください。");

                    while (true)
                    {
                        Thread.Sleep(200);
                        Application.DoEvents();
                        stat = pdc230.GetStatus();
                        if (stat != string.Empty)
                        {
                            break;
                        }
                        if (_canceled == true)
                        {
                            pdc230.Cancel();
                            break;
                        }
                    }

                    if (_canceled != true)
                    {

                        pdc230.CardRead();
                        DisplayDescription("カード読取中です。");

                        while (true)
                        {
                            Thread.Sleep(200);
                            Application.DoEvents();
                            data = pdc230.GetData();
                            if (data != string.Empty)
                            {
                                cardReaderInfo.Text = data;
                                break;
                            }
                            stat = pdc230.GetStatus();
                            if (stat != string.Empty)
                            {
                                break;
                            }
                        }

                        pdc230.Eject();
                                                
                        if (!(stat != string.Empty))
                        {

                            DisplayDescription("カード情報：" + data);

                            if (DialogResult.OK == MessageBox.Show("読み込んだカード情報を書込みますか？", "カード発行", MessageBoxButtons.OKCancel))
                            {
                                //カード情報書込
                                StatusPictureBox.Image = Properties.Resources.Blue;
                                IssueingModeChange();
                                IssueProc = true;
                                _canceled = false;
                                DisplayDescription("発行処理を行います。カードを挿入してください。");
                                pdc230.Insert();
                                while (true)
                                {
                                    Thread.Sleep(200);
                                    Application.DoEvents();
                                    stat = pdc230.GetStatus();
                                    if (stat != string.Empty)
                                    {
                                        break;
                                    }
                                    if (_canceled == true)
                                    {
                                        pdc230.Cancel();
                                        break;
                                    }
                                }
                                if (_canceled != true)
                                {
                                    DisplayDescription("発行処理中です。");
                                    WriteingModeChange();
                                    StatusPictureBox.Image = Properties.Resources.Orange;
                                    pdc230.CardWrite(cardReaderInfo.Text);
                                    while (true)
                                    {
                                        Thread.Sleep(200);
                                        Application.DoEvents();
                                        data = pdc230.GetData();
                                        if (data != string.Empty)
                                        {
                                            break;
                                        }
                                        stat = pdc230.GetStatus();
                                        if (stat != string.Empty)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                CardWriteProc = false;
                            }
                        }
                    }
                    pdc230.Close();
                    if (_canceled == true)
                    {
                        DisplayDescription("カード挿入待ちを中断しました。");
                        StatusPictureBox.Image = Properties.Resources.White;
                        InputModeChange();
                        _canceled = false;
                    }
                    else if (CardWriteProc == false)
                    {
                        DisplayDescription("カード書込みを中断しました。");
                        StatusPictureBox.Image = Properties.Resources.White;
                        InputModeChange();
                        _canceled = false;
                    }
                    else if (data != string.Empty)
                    {
                        DisplayingIssueMessages(data);
                        StatusPictureBox.Image = Properties.Resources.White;
                        InputModeChange();
                    }
                    else if (stat != string.Empty)
                    {
                        if (int.Parse(stat) >= 81)
                        {
                            StatusPictureBox.Image = Properties.Resources.Red;
                        }
                        else
                        {
                            StatusPictureBox.Image = Properties.Resources.Yellow;
                        }
                        DisplayingStatusMessages(stat);
                        InputModeChange();
                    }
                    IssueProc = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Properties.Resources.PDC230Error + "\n" + ex.Message, "システムエラー");
                    logger.Error(Properties.Resources.PDC230Error + ex.Message);
                    StatusPictureBox.Image = Properties.Resources.White;
                    InputModeChange();
                }
                finally
                {
                    pdc230.Close();
                    IssueProc = false;
                }

            }
            else
            {
                this.ActiveControl = null; // フォーム自体にフォーカスを移す
            }

        }
        //add 2024/12/11 end

        private bool _canceled = false;

        // 中断ボタンクリック
        private void CancelButton_Click(object sender, System.EventArgs e)
        {
            if (IssueProc == true)
            {
                _canceled = true;
            }
            else
            {
                if (NumberingProc == true)
                {                    
                    if (DialogResult.OK == MessageBox.Show("印鑑登録番号：" + NumberingTextBox.Text + "が無効になります。\r中断してもよろしいですか。", "発行中断", MessageBoxButtons.OKCancel))
                    {
                        _canceled = true;
                        //string data = (MunicipalCodeTextBox.Text + BranchOfficeTextBox.Text + NumberingTextBox.Text).PadRight(69, PaddingChar);   //del 2024/12/11                        
                        string data = (MunicipalCodeTextBox.Text + BranchOfficeTextBox.Text + NumberingTextBox.Text).PadRight(69, PaddingCharR);    //add 2024/12/11
                        DisplayingUnissuedMessage(data);
                        StatusPictureBox.Image = Properties.Resources.White;
                        InputModeChange();
                    }
                    else
                    {
                        IssueWaitModeChange();
                    }
                }
                else
                {
                    _canceled = true;

                    if (CardReadProc == false)      //add 2024/12/11
                    {
                        DisplayDescription("発行処理を中断しました。");
                        StatusPictureBox.Image = Properties.Resources.White;
                        InputModeChange();
                    }
                    //add 2024/12/11 str                    
                    else
                    {
                        DisplayDescription("カード読取を中断しました。");
                        StatusPictureBox.Image = Properties.Resources.White;
                        InputModeChange();
                    }
                    //add 2024/12/11 end
                }
            }

        }
        private void AboutBoxMenuItem_Click(object sender, System.EventArgs e)
        {
            AboutBox form = new AboutBox();
            form.ShowDialog();
        }
        private void IssueForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                NumberingButton.PerformClick();
            }
        }
        private void CancelButton_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\n')
            {
                IssueButton.PerformClick();
            }
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                    return true;
                case Keys.Return:
                    IssueButton.PerformClick();
                    return true;
                case Keys.Space:
                    NumberingButton.PerformClick();
                    return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void IssueForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_inputmode)
            {
                e.Cancel = true;
            }
        }

        // 通信設定
        private void MnuOptionCom_Click(object sender, EventArgs e)
        {
            DlgComSetting dlgCom = new DlgComSetting();

            //Dialogのオーナー設定
            dlgCom.Owner = this;

            dlgCom.ComPortNum = ComPortNum;     //COMポート番号を設定

            dlgCom.ShowDialog();                //通信設定ダイアログを表示

            bool res = dlgCom.DlgResult;

            //ダイアログを「設定」で終了した場合、設定値を取得する。
            if (res == true)
            {
                //ポート番号取得
                ComPortNum = dlgCom.ComPortNum;
                //ポート番号から名称を作成
                PortName = "COM" + ComPortNum.ToString();
                //COMポートの設定保存
                ClsIniFileHandler.WritePrivateProfileString("COM", "PORT", ComPortNum.ToString(), IniFilePath);
            }
        }
    }
}
