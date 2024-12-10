using System;
using System.Windows.Forms;
using Utils.Xml;
using EncodeData;

namespace ConfigFile {
	public partial class ConfigForm : Form {
		private XmlFile _SettingsFile;
		private XmlFile _NumberingMasterFile;
		private Settings _Settings = new Settings();
		private NumberingMaster _NumberingMaster = new NumberingMaster();
		public ConfigForm() {
			InitializeComponent();
						
			//
			// MunicipalCode 設定
			//
			MunicipalCodeTextBox.MaxLength = (int)MunicipalCodeLengthUpDown.Value;

			//
			// BranchOffice 設定
			//
			BranchOfficeTextBox.MaxLength = (int)BranchOfficeLengthUpDown.Value;

			//
			// SealRegistration 設定
			//
			NumberingMasterTextBox.MaxLength = (int)SealRegistrationLengthUpDown.Value;

			if (Properties.Settings.Default.SealRegistration_PaddingCharacter == '0') {
				LeftPaddingZeroRadioButton.Checked = true;
			}
			else {
				LeftPaddingSpaceRadioButton.Checked = true;
			}

			//
			// Padding 設定
			//

			if (Properties.Settings.Default.Padding_PaddingStatus == PaddingStat.None) {
				PaddingCheckBox.Checked = false;
				RightPaddingZeroRadioButton.Enabled = false;
				RightPaddingSpaceRadioButton.Enabled = false;
			}
			else {
				PaddingCheckBox.Checked = true;
				RightPaddingZeroRadioButton.Enabled = true;
				RightPaddingSpaceRadioButton.Enabled = true;
			}

			if (Properties.Settings.Default.Padding_PaddingCharacter == '0') {
				RightPaddingZeroRadioButton.Checked = true;
			}
			else {
				RightPaddingSpaceRadioButton.Checked = true;
			}
						
		}


		private void MakeFileButton_Click(object sender, EventArgs e) {

			//			if (MunicipalCodeTextBox.Text.Length < Properties.Settings.Default.MunicipalCodeLengthMin) {
//			if (MunicipalCodeTextBox.Text.Length < Properties.Settings.Default.MunicipalCode_LengthMin) {
			if (MunicipalCodeTextBox.Text.Length < MunicipalCodeLengthUpDown.Value) {
				//MessageBox.Show("ヘッダ桁数が、必要桁数に達していません。");				//del 2024/08/09				
				MessageBox.Show("ヘッダ桁数が、必要桁数に達していません。","入力エラー");   //add 2024/08/09
				return;
			}
			//			if (BranchOfficeTextBox.Text.Length < Properties.Settings.Default.BranchOfficeLengthMin) {
//			if (BranchOfficeTextBox.Text.Length < Properties.Settings.Default.BranchOffice_LengthMin) {
			if (BranchOfficeTextBox.Text.Length < BranchOfficeLengthUpDown.Value) {
				//MessageBox.Show("拠点コード桁数が、必要桁数に達していません。");				//del 2024/08/09				
				MessageBox.Show("拠点コード桁数が、必要桁数に達していません。","入力エラー");   //add 2024/08/09
				return;
			}

			_Settings.MunicipalCode.Value = MunicipalCodeTextBox.Text;
			_Settings.MunicipalCode.Length = MunicipalCodeLengthUpDown.Value;
			_Settings.MunicipalCode.PaddingStatus = Properties.Settings.Default.MunicipalCode_PaddingStatus;
			_Settings.MunicipalCode.PaddingCharacter = Properties.Settings.Default.MunicipalCode_PaddingCharacter;

			_Settings.BranchOffice.Value = BranchOfficeTextBox.Text;
			_Settings.BranchOffice.Length = BranchOfficeLengthUpDown.Value;
			_Settings.BranchOffice.PaddingStatus = Properties.Settings.Default.BranchOffice_PaddingStatus;
			_Settings.BranchOffice.PaddingCharacter = Properties.Settings.Default.BranchOffice_PaddingCharacter;

			_Settings.SealRegistration.Value = NumberingMasterTextBox.Text;
			_Settings.SealRegistration.Length = SealRegistrationLengthUpDown.Value;
			_Settings.SealRegistration.PaddingStatus = Properties.Settings.Default.SealRegistration_PaddingStatus;
			if (LeftPaddingZeroRadioButton.Checked == true) {
				_Settings.SealRegistration.PaddingCharacter = '0';
			}
			else{
				_Settings.SealRegistration.PaddingCharacter = ' ';
			}

			_Settings.Padding.Value = Properties.Settings.Default.Padding_Value;
			_Settings.Padding.Length = Properties.Settings.Default.Padding_Length;
			if (PaddingCheckBox.Checked == true) {
				_Settings.Padding.PaddingStatus = PaddingStat.Right;
			}
			else {
				_Settings.Padding.PaddingStatus = PaddingStat.None;
			}
			if (RightPaddingZeroRadioButton.Checked == true) {
				_Settings.Padding.PaddingCharacter = '0';
			}
			else {
				_Settings.Padding.PaddingCharacter = ' ';
			}


			//			_Settings.MunicipalCode.Value = MunicipalCodeTextBox.Text;
			//_Settings.BranchOffice.Value = BranchOfficeTextBox.Text;

			//_Settings.MunicipalCode.Length = MunicipalCodeTextBox.Text.Length;
			//			_Settings.BranchOffice.Length = BranchOfficeTextBox.Text.Length;
			//_Settings.SealRegistration.Length = (int)SealRegistrationLengthUpDown.Value;

			//if (LeftPaddingZeroRadioButton.Checked == true) {
			//	_Settings.SealRegistration.PaddingCharacter = '0';
			//}
			//else {
			//	_Settings.SealRegistration.PaddingCharacter = ' ';
			//}

			if (SaveConfigFileDialog.ShowDialog() == DialogResult.OK) {
				_SettingsFile = new XmlFile(SaveConfigFileDialog.FileName, _Settings);
				_SettingsFile.Write(_Settings);
				Properties.Settings.Default.Save();
			}
		}

		private void NumberingMasterButton_Click(object sender, EventArgs e) {
			if (NumberingMasterTextBox.Text == string.Empty) {
				//MessageBox.Show("カード番号 初期値が入力されていません。");				//del 2024/08/09				
				MessageBox.Show("カード番号 初期値が入力されていません。","設定エラー");    //add 2024/08/09
				return;
			}


			_NumberingMaster.Number = ulong.Parse(NumberingMasterTextBox.Text);
			if (SaveNumberingMasterFileDialog.ShowDialog() == DialogResult.OK) {
				_NumberingMasterFile = new XmlFile(SaveNumberingMasterFileDialog.FileName, _NumberingMaster);
				_NumberingMasterFile.Write(_NumberingMaster);
			}
		}

		private void MunicipalCodeLengthUpDown_ValueChanged(object sender, EventArgs e) {
			MunicipalCodeTextBox.Text = string.Empty;
			MunicipalCodeTextBox.MaxLength = (int)MunicipalCodeLengthUpDown.Value;
			
		}

		private void BranchOfficeLengthUpDown_ValueChanged(object sender, EventArgs e) {
			BranchOfficeTextBox.Text = string.Empty;
			BranchOfficeTextBox.MaxLength = (int)BranchOfficeLengthUpDown.Value;
		}

		private void SealRegistrationLengthUpDown_ValueChanged(object sender, EventArgs e) {
			NumberingMasterTextBox.Text = string.Empty;
			NumberingMasterTextBox.MaxLength = (int)SealRegistrationLengthUpDown.Value;
		}

		private void PaddingCheckBox_CheckedChanged(object sender, EventArgs e) {
			RightPaddingZeroRadioButton.Enabled = PaddingCheckBox.Checked;
			RightPaddingSpaceRadioButton.Enabled = PaddingCheckBox.Checked;
		}
	}
}
