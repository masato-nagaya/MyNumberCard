﻿using System.Windows.Forms;
using ConfigFile;
using PDC230Lib;

using System.Threading;
using System.IO.Ports;
using Utils.Form;
using Utils.Xml;
using log4net;
using log4net.Config;
using System.IO;
using log4net.Appender;
using System;
using EncodeData;
using PDC_230;
using System.Reflection;

namespace MyNumberCard
{

    public partial class IssueForm : Form
    {

        private ILog logger;

        private XmlFile _SettingsFile;
        private XmlFile _NumberingMasterFile;
        private Settings _Settings = new Settings();
        private NumberingMaster _NumberingMaster = new NumberingMaster();

        private bool NumberingProc = true;
        private bool IssueProc = false;

        private char PaddingChar;

        //add 2024/08/09 str
        string IniFilePath;
        private static int ComPortNum = 1;
        private static string PortName;
        //add 2024/08/09 end

        public IssueForm()
        {

            InitializeComponent();

            //this.Font = Properties.Settings.Default.Font;
            //this.MenuStrip.Font = Properties.Settings.Default.Font;
            //this.StatusStrip.Font = Properties.Settings.Default.Font;
            //this.StatusLabel.Font = Properties.Settings.Default.Font;

            string logFolder = Properties.Settings.Default.IssueLogPath;
            InitializeLog4Net(logFolder);
            logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            //add 2024/08/09 str
            Assembly myAssembly = Assembly.GetEntryAssembly();
            string path = myAssembly.Location;
            string[] sAray = path.Split('\\');
            sAray[sAray.Length - 1] = @"PDC-230.ini";
            IniFilePath = string.Join(@"\", sAray);

            //Portの読み込み
            if (File.Exists(IniFilePath))
            {
                ComPortNum = (int)ClsIniFileHandler.GetPrivateProfileInt("COM", "PORT", ComPortNum, IniFilePath);
                PortName = "COM" + ComPortNum.ToString();  //ポート番号から名称を作成
            }

            //add 2024/08/09 end

            InputModeChange();
            try
            {
                string confpath = Properties.Settings.Default.ConfigFilePath;
                _SettingsFile = new XmlFile(confpath, _Settings);
                _Settings = (Settings)_SettingsFile.Read();

                //MunicipalCodeTextBox.MaxLength = _Settings.WriteFormat.MunicipalCode.Length;
                //MunicipalCodeTextBox.Text = _Settings.CodeValue.MunicipalCode;

                //BranchOfficeTextBox.MaxLength = _Settings.WriteFormat.BranchOffice.Length;
                //BranchOfficeTextBox.Text = _Settings.CodeValue.BranchOffice;

                //NumberingTextBox.MaxLength = _Settings.WriteFormat.SealRegistration.Length;


                MunicipalCodeTextBox.MaxLength = (int)_Settings.MunicipalCode.Length;
                MunicipalCodeTextBox.Text = _Settings.MunicipalCode.Value;

                BranchOfficeTextBox.MaxLength = (int)_Settings.BranchOffice.Length;
                BranchOfficeTextBox.Text = _Settings.BranchOffice.Value;

                NumberingTextBox.MaxLength = (int)_Settings.SealRegistration.Length;

                PaddingChar = _Settings.Padding.PaddingCharacter;

                string numpath = Properties.Settings.Default.NumberingMasterPath;
                _NumberingMasterFile = new XmlFile(numpath, _NumberingMaster);

                logger.Debug("ConfPath:" + confpath);
                logger.Debug("NumPath:" + numpath);

            }
            catch (Exception)
            {
                //MessageBox.Show(Properties.Resources.NotFoundConfigFile + "\nシステム管理者に問い合わせてください。");				//del 2024/08/09				
                MessageBox.Show(Properties.Resources.NotFoundConfigFile + "\nシステム管理者に問い合わせてください。", "システムエラー");  //add 2024/08/09
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

        private string[] GetPortNames()
        {

            //add 2024/08/09 str

            //string[] port = PDC230.GePortNames();

            string[] port = new string[0];

            if (PortName != null)
            {
                port = new string[1];
                port[0] = PortName;
            }

            //add 2024/08/09 end

            if (0 == port.Length)
            {
                //未設定
                string message = Properties.Resources.PDC230Disconnected;
                //MessageBox.Show(message);				//del 2024/08/09				
                MessageBox.Show(message, "接続未設定"); //add 2024/08/09
                logger.Error(message);
                port = null;
            }
            else if (1 < port.Length)
            {
                //複数端末接続
                string message = Properties.Resources.PDC230MultipleConnections;
                //MessageBox.Show(message);				//del 2024/08/09				
                MessageBox.Show(message, "接続エラー");  //add 2024/08/09
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
            IssueButton.Enabled = false;
            CancelButton.Enabled = false;

            NumberingTextBox.ReadOnly = true;
            _inputmode = true;
        }

        private void IssueingModeChange()
        {
            NumberingButton.Enabled = false;
            ManualInputButton.Enabled = false;
            IssueButton.Enabled = false;
            CancelButton.Enabled = true;
            _inputmode = false;
        }


        private void WriteingModeChange()
        {
            NumberingButton.Enabled = false;
            ManualInputButton.Enabled = false;
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

        private void NumberingButton_Click(object sender, System.EventArgs e)
        {
            IssueWaitModeChange();
            try
            {
                _NumberingMaster = (NumberingMaster)_NumberingMasterFile.Read();
                ulong num = _NumberingMaster.Number;
                //				int len = _Settings.WriteFormat.SealRegistration.Length;
                int len = (int)_Settings.SealRegistration.Length;
                if ((num).ToString().Length == len + 1)
                {
                    string msg = "カード番号が上限に達しました。\nシステム管理者に問い合わせてください。";
                    //MessageBox.Show(msg);				//del 2024/08/09					
                    MessageBox.Show(msg, "採番エラー");  //add 2024/08/09
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
            catch (Exception)
            {
                //MessageBox.Show(Properties.Resources.NotFoundConfigFile + "\nシステム管理者に問い合わせてください。");				//del 2024/08/09				
                MessageBox.Show(Properties.Resources.NotFoundConfigFile + "\nシステム管理者に問い合わせてください。", "システムエラー"); //add 2024/08/09
                logger.Error(Properties.Resources.NotFoundConfigFile);
                _inputmode = true;
                Application.Exit();
            }
        }

        private void ManualInputButton_Click(object sender, System.EventArgs e)
        {
            IssueWaitModeChange();
            NumberingTextBox.Text = string.Empty;
            NumberingTextBox.ReadOnly = false;

            DisplayDescription("カード番号を入力してください。");
            NumberingProc = false;
            NumberingTextBox.Focus();

        }


        private string NumberingPadding()
        {
            //int width = _Settings.WriteFormat.SealRegistration.Length;
            //char padding = _Settings.WriteFormat.SealRegistration.LeadingCharacter;
            int width = (int)_Settings.SealRegistration.Length;
            char padding = _Settings.SealRegistration.PaddingCharacter;
            return NumberingTextBox.Text.PadLeft(width, padding);
        }

        // 発行ボタンクリック
        private void IssueButton_Click(object sender, System.EventArgs e)
        {
            StatusPictureBox.Image = Properties.Resources.Blue;
            IssueingModeChange();

            NumberingTextBox.Text = NumberingPadding();

            string[] port = GetPortNames();

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
                if (Properties.Settings.Default.PortNmae != string.Empty)
                {
                    StatusPictureBox.Image = Properties.Resources.White;
                    IssueWaitModeChange();
                    return;
                }
                port[0] = Properties.Settings.Default.PortNmae;
            }

            IssueProc = true;
            _canceled = false;
            string data = string.Empty;
            string stat = string.Empty;
            PDC230 pdc230 = new PDC230(port[0], 9600, Parity.Even, 8, StopBits.One);

            try
            {
                pdc230.Open();
                pdc230.Cancel();
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
                    DisplayDescription("発行処理処理中です。");
                    WriteingModeChange();
                    StatusPictureBox.Image = Properties.Resources.Orange;
                    //				pdc230.CardWrite(MunicipalCodeTextBox.Text + BranchOfficeTextBox.Text + NumberingTextBox.Text);
                    pdc230.CardWrite(MunicipalCodeTextBox.Text + BranchOfficeTextBox.Text + NumberingTextBox.Text, PaddingChar);
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
                //MessageBox.Show(Properties.Resources.PDC230Error + "\n" + ex.Message);				//del 2024/08/09					
                MessageBox.Show(Properties.Resources.PDC230Error + "\n" + ex.Message, "システムエラー"); //add 2024/08/09
                logger.Error(Properties.Resources.PDC230Error + ex.Message);
                StatusPictureBox.Image = Properties.Resources.White;
                IssueWaitModeChange();
                //_inputmode = true;
                //Application.Exit();
            }
            finally
            {
                pdc230.Close();
                IssueProc = false;
            }
        }

        private bool _canceled = false;
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
                    if (DialogResult.OK == MessageBox.Show("カード番号：" + NumberingTextBox.Text + "が無効になります。\r中断してもよろしいですか。", "発行中断", MessageBoxButtons.OKCancel))
                    {
                        _canceled = true;
                        string data = (MunicipalCodeTextBox.Text + BranchOfficeTextBox.Text + NumberingTextBox.Text).PadRight(69, PaddingChar);
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
                    DisplayDescription("発行処理を中断しました。");
                    StatusPictureBox.Image = Properties.Resources.White;
                    InputModeChange();
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

        //add 2024/08/09 str
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
                ComPortNum = dlgCom.ComPortNum;
                PortName = "COM" + ComPortNum.ToString();  //ポート番号から名称を作成

                //COMポートの設定保存
                ClsIniFileHandler.WritePrivateProfileString("COM", "PORT", ComPortNum.ToString(), IniFilePath);
            }
        }

        private void IssueForm_Load(object sender, EventArgs e)
        {

        }
        //add 2024/08/09 end
    }
}
