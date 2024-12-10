using System;
using System.Windows.Forms;
using MyNumberCard.Properties;
using System.IO;
using System.Threading;

namespace MyNumberCard {
	public static class Program {
		//private static ILog logger;
		
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main(string[] args) {

			Mutex mutex = new Mutex(false, "MyNumberCard");
			if (mutex.WaitOne(0, false) == false) {
				return;
			}
			if (args.Length == 1) {
				if (args[0] == "/C") {
					Settings.Default.Reset();
				}
				if (args[0].IndexOf("/COM") >= 0) {
					Settings.Default.PortNmae = args[0];
				}
			}

			if (true == IsFirstSetting()) {
				if (Result.OK == FirstSetting()){
					//MessageBox.Show(Resources.RestartMessage);				//del 2024/08/09					
					MessageBox.Show(Resources.RestartMessage, "設定完了");      //add 2024/08/09
					Application.Exit();
					System.Diagnostics.Process.Start(Application.ExecutablePath);
				}
			}
			else {
				if (ExistConfigFile()) {
					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					Application.Run(new IssueForm());
				}
				else {
					//MessageBox.Show(Resources.NotFoundConfigFile + "\nシステム管理者に問い合わせてください。);					//del 2024/08/09					
					MessageBox.Show(Resources.NotFoundConfigFile + "\nシステム管理者に問い合わせてください。", "システムエラー");	//add 2024/08/09
				}
			}
			mutex.ReleaseMutex();
		}

		private static bool IsFirstSetting() {
			return (Settings.Default.ConfigFilePath.Equals(string.Empty) ||
					Settings.Default.NumberingMasterPath.Equals(string.Empty) ||
					Settings.Default.IssueLogPath.Equals(string.Empty));
		}

		private static bool ExistConfigFile() {
			string configpath = Settings.Default.ConfigFilePath;
			string numberingpath = Settings.Default.NumberingMasterPath;
			string issuelogpath = Settings.Default.IssueLogPath;

			return (File.Exists(configpath) && File.Exists(numberingpath) && Directory.Exists(issuelogpath));
		}

		private static Result FirstSetting() {
			Result result = Result.Cancel;
			if (DialogResult.OK == MessageBox.Show(Resources.FirstSettingMessage,Resources.FirstSettiingTitle,MessageBoxButtons.OKCancel)) {
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);

				OwnSettingForm form = new OwnSettingForm();
				Application.Run(form);
				result = form.Result;
				form = null;
			}
			return result;
		}
	}

}
