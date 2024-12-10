using System;
using System.Windows.Forms;
using MyNumberCard.Properties;
namespace MyNumberCard {
	public enum Result {
		OK,
		Cancel
	}
	public partial class OwnSettingForm : Form {

		public Result Result { get; set; }
			
		public OwnSettingForm() {
			InitializeComponent();
//			this.Font = Properties.Settings.Default.Font;

			Result = Result.Cancel;

			ConfigPathTextBox.Text = Settings.Default.ConfigFilePath;
			NumberingMasterPathTextBox.Text = Settings.Default.NumberingMasterPath;
			IssueLogPathTextBox.Text = Settings.Default.IssueLogPath;
		}

		private void OpenConfigFileDialogButton_Click(object sender, EventArgs e) {
			if (OpenConfigFileDialog.ShowDialog() == DialogResult.OK) {
				ConfigPathTextBox.Text = OpenConfigFileDialog.FileName;
			}
		}

		private void OpenNumberingMasterFileButton_Click(object sender, EventArgs e) {
			if (OpenNumberingMasterFileDialog.ShowDialog() == DialogResult.OK) {
				NumberingMasterPathTextBox.Text = OpenNumberingMasterFileDialog.FileName;
			}

		}

		private void OpenIssueLogFileButton_Click(object sender, EventArgs e) {
			if (IssueLogFolderBrowserDialog.ShowDialog() == DialogResult.OK) {
				IssueLogPathTextBox.Text = IssueLogFolderBrowserDialog.SelectedPath;
			}
		}

		private void OkButton_Click(object sender, EventArgs e) {

			Settings.Default.ConfigFilePath = ConfigPathTextBox.Text;
			Settings.Default.NumberingMasterPath = NumberingMasterPathTextBox.Text;
			Settings.Default.IssueLogPath = IssueLogPathTextBox.Text;
			Settings.Default.Save();

			Result = Result.OK;
			this.Close();
		}

		private void CancelButton_Click(object sender, EventArgs e) {
			this.Close();
		}

	}
}
