using System;
using System.Windows.Forms;
using Utils.Xml;
using EncodeData;
using System.Text.RegularExpressions;

namespace ConfigFile
{
    public partial class ConfigForm : Form
    {
        private XmlFile _SettingsFile;
        private XmlFile _NumberingMasterFile;
        private Settings _Settings = new Settings();
        private NumberingMaster _NumberingMaster = new NumberingMaster();
        public ConfigForm()
        {
            InitializeComponent();
                        
            // ヘッダー設定            
            MunicipalCodeTextBox.MaxLength = (int)MunicipalCodeLengthUpDown.Value;
                        
            // 拠点コード設定            
            BranchOfficeTextBox.MaxLength = (int)BranchOfficeLengthUpDown.Value;

            // 印鑑登録番号設定            
            NumberingMasterTextBox.MaxLength = (int)SealRegistrationLengthUpDown.Value;

            //add 2024/12/11 str

            //if (Properties.Settings.Default.SealRegistration_PaddingCharacter == '0')
            //{
            //    LeftPaddingZeroRadioButton.Checked = true;
            //}
            //else
            //{
            //    LeftPaddingSpaceRadioButton.Checked = true;
            //}

            // 印鑑登録番号 左 データ埋め設定
            if (Properties.Settings.Default.PaddingL_PaddingStatus == PaddingStat.None)
            {
                LeftPaddingCheckBox.Checked = false;
                LeftPaddingZeroRadioButton.Enabled = false;
                LeftPaddingSpaceRadioButton.Enabled = false;
            }
            else
            {
                LeftPaddingCheckBox.Checked = true;
                LeftPaddingZeroRadioButton.Enabled = true;
                LeftPaddingSpaceRadioButton.Enabled = true;
            }

            if (Properties.Settings.Default.PaddingL_PaddingCharacter == '0')
            {
                LeftPaddingZeroRadioButton.Checked = true;
            }
            else
            {
                LeftPaddingSpaceRadioButton.Checked = true;
            }

            //add 2024/12/11 end

            // 印鑑登録番号 右 データ埋め 設定
            if (Properties.Settings.Default.Padding_PaddingStatus == PaddingStat.None)
            {
                PaddingCheckBox.Checked = false;
                RightPaddingZeroRadioButton.Enabled = false;
                RightPaddingSpaceRadioButton.Enabled = false;
            }
            else
            {
                PaddingCheckBox.Checked = true;
                RightPaddingZeroRadioButton.Enabled = true;
                RightPaddingSpaceRadioButton.Enabled = true;
            }

            if (Properties.Settings.Default.Padding_PaddingCharacter == '0')
            {
                RightPaddingZeroRadioButton.Checked = true;
            }
            else
            {
                RightPaddingSpaceRadioButton.Checked = true;
            }
            
        }
        private void MakeFileButton_Click(object sender, EventArgs e)
        {
            //登録可能桁数
            decimal registerableDigits = 70 - StartPositionDigitUpDown.Value;

            //ヘッダー桁数のチェック
            if (MunicipalCodeTextBox.Text.Length < MunicipalCodeLengthUpDown.Value)
            {                
                MessageBox.Show("ヘッダ桁数が、必要桁数に達していません。", "入力エラー");
                return;
            }
            //ヘッダー入力値のチェック
            if (!ValidateMunicipalCodeText(MunicipalCodeTextBox.Text))
            {
                MessageBox.Show("ヘッダの入力値に無効な値が含まれています。", "入力エラー");
                return;
            }

            //拠点コードにチェックがついていればバリデーションチェックを行う
            if (BranchOfficeCheckBox.Checked == true)
            {
                //桁数のチェック
                if (BranchOfficeTextBox.Text.Length < BranchOfficeLengthUpDown.Value)
                {
                    MessageBox.Show("拠点コード桁数が、必要桁数に達していません。", "入力エラー");
                    return;
                }
                //入力値のチェック
                if (!ValidateBranchOfficeText(BranchOfficeTextBox.Text))
                {
                    MessageBox.Show("拠点コードの入力値に無効な値が含まれています。", "入力エラー");
                    return;
                }
            }

            //開始位置が問題ないかチェックする
            if (BranchOfficeCheckBox.Checked == true)
            {
                //入力された桁数の合計
                decimal inputTotalDigits;
                inputTotalDigits = MunicipalCodeLengthUpDown.Value + BranchOfficeLengthUpDown.Value + SealRegistrationLengthUpDown.Value;
                //開始位置がおかしければリターン
                if (inputTotalDigits > registerableDigits)
                {
                    MessageBox.Show("開始位置の値が不正です。", "入力エラー");
                    return;
                }
            }
            else
            {
                //入力された桁数の合計
                decimal inputTotalDigits;
                inputTotalDigits = MunicipalCodeLengthUpDown.Value + SealRegistrationLengthUpDown.Value;
                //開始位置がおかしければリターン
                if (inputTotalDigits > registerableDigits)
                {
                    MessageBox.Show("開始位置の値が不正です。", "入力エラー");
                    return;
                }
            }

            //add 2024/12/11 str
            
            //開始位置
            _Settings.StartPosition.Value = StartPositionDigitUpDown.Value.ToString();
            _Settings.StartPosition.Length = Properties.Settings.Default.StartPosition_Length;
            _Settings.StartPosition.PaddingStatus = Properties.Settings.Default.StartPosition_PaddingStatus;
            _Settings.StartPosition.PaddingCharacter = Properties.Settings.Default.StartPosition_PaddingCharacter;

            //add 2024/12/11 end

            //ヘッダー
            _Settings.MunicipalCode.Value = MunicipalCodeTextBox.Text;
            _Settings.MunicipalCode.Length = MunicipalCodeLengthUpDown.Value;
            _Settings.MunicipalCode.PaddingStatus = Properties.Settings.Default.MunicipalCode_PaddingStatus;
            _Settings.MunicipalCode.PaddingCharacter = Properties.Settings.Default.MunicipalCode_PaddingCharacter;

            //拠点コード

            //add 2024/12/11 str            
            
            //_Settings.BranchOffice.Value = BranchOfficeTextBox.Text;
            //_Settings.BranchOffice.Length = BranchOfficeLengthUpDown.Value;
            
            if (BranchOfficeCheckBox.Checked == true)
            {
                _Settings.BranchOffice.Value = BranchOfficeTextBox.Text;
                _Settings.BranchOffice.Length = BranchOfficeLengthUpDown.Value;                
                _Settings.BranchOffice.PaddingStatus = PaddingStat.ON;
            }
            else
            {
                _Settings.BranchOffice.Value = "0";
                _Settings.BranchOffice.Length = 0;
                _Settings.BranchOffice.PaddingStatus = Properties.Settings.Default.BranchOffice_PaddingStatus;
                _Settings.BranchOffice.PaddingStatus = PaddingStat.None;
            }            
            _Settings.BranchOffice.PaddingCharacter = Properties.Settings.Default.BranchOffice_PaddingCharacter;

            //add 2024/12/11 end

            //印鑑登録番号
            _Settings.SealRegistration.Value = NumberingMasterTextBox.Text;
            _Settings.SealRegistration.Length = SealRegistrationLengthUpDown.Value;

            //add 2024/12/11 str

            //_Settings.SealRegistration.PaddingStatus = Properties.Settings.Default.SealRegistration_PaddingStatus;
            //if (LeftPaddingCheckBox.Checked == true)
            //{
            //    _Settings.SealRegistration.PaddingStatus = PaddingStat.Left;
            //}
            //else
            //{
            //    _Settings.SealRegistration.PaddingStatus = PaddingStat.None;
            //}
            //if (LeftPaddingZeroRadioButton.Checked == true)
            //{
            //    _Settings.SealRegistration.PaddingCharacter = '0';
            //}
            //else
            //{
            //    _Settings.SealRegistration.PaddingCharacter = ' ';
            //}

            _Settings.SealRegistration.PaddingStatus = PaddingStat.None;
            _Settings.SealRegistration.PaddingCharacter = Properties.Settings.Default.SealRegistration_PaddingCharacter;

            //印鑑登録番号 左	 データ埋め            
            _Settings.PaddingLeft.Value = Properties.Settings.Default.PaddingL_Value;
            _Settings.PaddingLeft.Length = Properties.Settings.Default.PaddingL_Length;
            if (LeftPaddingCheckBox.Checked == true)
            {
                _Settings.PaddingLeft.PaddingStatus = PaddingStat.Left;
            }
            else
            {
                _Settings.PaddingLeft.PaddingStatus = PaddingStat.None;
            }
            if (LeftPaddingZeroRadioButton.Checked == true)
            {
                _Settings.PaddingLeft.PaddingCharacter = '0';
            }
            else
            {
                _Settings.PaddingLeft.PaddingCharacter = ' ';
            }

            //add 2024/12/11 end

            //印鑑登録番号 右	 データ埋め
            _Settings.Padding.Value = Properties.Settings.Default.Padding_Value;
            _Settings.Padding.Length = Properties.Settings.Default.Padding_Length;
            if (PaddingCheckBox.Checked == true)
            {
                _Settings.Padding.PaddingStatus = PaddingStat.Right;
            }
            else
            {
                _Settings.Padding.PaddingStatus = PaddingStat.None;
            }
            if (RightPaddingZeroRadioButton.Checked == true)
            {
                _Settings.Padding.PaddingCharacter = '0';
            }
            else
            {
                _Settings.Padding.PaddingCharacter = ' ';
            }
            // 保存場所が決定されればxmlファイル出力
            if (SaveConfigFileDialog.FileName == "Config.xml")
            { 
            }
            else
            {
                SaveConfigFileDialog.FileName = "Config.xml";
            }
            if (SaveConfigFileDialog.ShowDialog() == DialogResult.OK)
            {
                _SettingsFile = new XmlFile(SaveConfigFileDialog.FileName, _Settings);
                _SettingsFile.Write(_Settings);
                Properties.Settings.Default.Save();
            }
        }        
        // ヘッダーのバリデーションチェック        
        private bool ValidateMunicipalCodeText(string inputText)
        {
            // 半角数字、半角スペース、大文字Sのみ許可
            string pattern = @"^[0-9S\s]+$";
            return Regex.IsMatch(inputText, pattern);
        }                
        // 拠点コードのバリデーションチェック        
        private bool ValidateBranchOfficeText(string inputText)
        {
            // 半角数字、半角スペース、大文字アルファベットのみ許可
            string pattern = @"^[0-9A-Z\s]+$";
            return Regex.IsMatch(inputText, pattern);
        }
        // 採番マスタファイル 作成
        private void NumberingMasterButton_Click(object sender, EventArgs e)
        {
            if (NumberingMasterTextBox.Text == string.Empty)
            {                
                MessageBox.Show("カード番号 初期値が入力されていません。", "設定エラー");
                return;
            }
            _NumberingMaster.Number = ulong.Parse(NumberingMasterTextBox.Text);
            if (SaveNumberingMasterFileDialog.FileName == "NumberingMaster.xml")
            {
            }
            else
            {
                SaveNumberingMasterFileDialog.FileName = "NumberingMaster.xml";
            }
            if (SaveNumberingMasterFileDialog.ShowDialog() == DialogResult.OK)
            {
                _NumberingMasterFile = new XmlFile(SaveNumberingMasterFileDialog.FileName, _NumberingMaster);
                _NumberingMasterFile.Write(_NumberingMaster);
            }
        }

        private void MunicipalCodeLengthUpDown_ValueChanged(object sender, EventArgs e)
        {
            MunicipalCodeTextBox.Text = string.Empty;
            MunicipalCodeTextBox.MaxLength = (int)MunicipalCodeLengthUpDown.Value;
        }
        private void BranchOfficeLengthUpDown_ValueChanged(object sender, EventArgs e)
        {
            BranchOfficeTextBox.Text = string.Empty;
            BranchOfficeTextBox.MaxLength = (int)BranchOfficeLengthUpDown.Value;
        }
        private void SealRegistrationLengthUpDown_ValueChanged(object sender, EventArgs e)
        {
            NumberingMasterTextBox.Text = string.Empty;
            NumberingMasterTextBox.MaxLength = (int)SealRegistrationLengthUpDown.Value;
        }
        private void PaddingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            RightPaddingZeroRadioButton.Enabled = PaddingCheckBox.Checked;
            RightPaddingSpaceRadioButton.Enabled = PaddingCheckBox.Checked;
        }
        private void LeftPaddingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            LeftPaddingZeroRadioButton.Enabled = LeftPaddingCheckBox.Checked;
            LeftPaddingSpaceRadioButton.Enabled = LeftPaddingCheckBox.Checked;
        }
        private void BranchOfficeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            BranchOfficeLengthUpDown.Enabled = BranchOfficeCheckBox.Checked;
            BranchOfficeTextBox.Enabled = BranchOfficeCheckBox.Checked;
        }
        
    }
}
