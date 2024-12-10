using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

using System.Management;

namespace MyNumberCard
{
    public partial class DlgComSetting : Form
    {

		private bool pDlgResult = false;

		public DlgComSetting()
        {
            string[] strPortName = new string[1];
            string[] strDeviceName = new string[1];
            int[] iPortNum = new int[1];
            string sCom = "COM";

            InitializeComponent();

			//存在するCOMポートを取得
			int i = 0;
			foreach (string s in SerialPort.GetPortNames())
			{
				try
				{
#if false
					// 例外のデバッグのため、例外をスロー
					if ((i == 2) && (bThrowExc1 == false)) {
						bThrowExc1 = true;
						throw new System.Exception();
					}
#endif
					Array.Resize(ref iPortNum, i + 1);
					string sTmp = s;
					string sComNo = sTmp.Substring(sCom.Length, sTmp.Length - sCom.Length);
					iPortNum[i] = int.Parse(sComNo);
					i++;
				}
				catch
				{
					continue;
				}
			}

			//ポート番号でソート
			for (int j = 0; j < iPortNum.Length; j++)
			{
				for (int k = 0; k < (iPortNum.Length - 1); k++)
				{
					if (iPortNum[k] > iPortNum[j])
					{
						int tmp = iPortNum[j];
						iPortNum[j] = iPortNum[k];
						iPortNum[k] = tmp;
					}
				}
			}

			//ポート番号をCOM**にする
			Array.Resize(ref strPortName, iPortNum.Length);

			for (int j = 0; j < iPortNum.Length; j++)
			{
				strPortName[j] = sCom + iPortNum[j].ToString();
			}

			//COM**のデバイス名を取得する
			i = 0;
			ManagementClass mcPnPEntity = new ManagementClass("Win32_PnPEntity");
			ManagementObjectCollection manageObjCol = mcPnPEntity.GetInstances();

			foreach (ManagementObject manageObj in manageObjCol)
			{
				try
				{
#if false
					// 例外のデバッグのため、例外をスロー
					if ((i == 2) && (bThrowExc2 == false)) {
						bThrowExc2 = true;
						throw new System.Exception();
					}
#endif
					//Nameプロパティを取得
					var namePropertyValue = manageObj.GetPropertyValue("Name");
					if (namePropertyValue == null)
					{
						continue;
					}

					//Nameプロパティ文字列の一部が"(COM1)～(COM999)"と一致するときリストに追加"
					string name = namePropertyValue.ToString();

					Array.Resize(ref strDeviceName, i + 1);

					strDeviceName[i++] = name;
				}
				catch
				{
					continue;
				}
			}

			//COM**とデバイス名を接続し、アイテムに追加する
			for (int j = 0; j < strPortName.Length; j++)
			{
				for (int k = 0; k < strDeviceName.Length; k++)
				{
					if (string.IsNullOrEmpty(strPortName[j]) == false)
					{
						if (strDeviceName[k].IndexOf(strPortName[j]) != -1)
						{
							CmbPort.Items.Add(strPortName[j] + ":" + strDeviceName[k]);
						}
					}
				}
			}
		}
				
		public bool DlgResult   //ダイアログの結果の取得
		{
			get { return pDlgResult; }
		}


		public int ComPortNum
		{
			get
			{
				string sPortNum = "";
				string sCom = "COM";

				string SelectedItem = CmbPort.SelectedItem.ToString();

				//CombBoxのItemから"COM**"を取り出し、"**"を取得しポート番号とする。
				string[] sAray = SelectedItem.Split(':');
				sPortNum = sAray[0].Substring(sCom.Length, sAray[0].Length - sCom.Length);

				return int.Parse(sPortNum);
			}
			set
			{
				int pComPortNum = value;

				string sPortName = "COM" + pComPortNum.ToString();

				//ComBoxのItemの中からPortNoを探し選択する。
				for (int i = 0; i < CmbPort.Items.Count; i++)
				{
					CmbPort.SelectedIndex = i;
					string SelectedItem = CmbPort.SelectedItem.ToString();
					if (SelectedItem.IndexOf(sPortName) >= 0)
					{
						break;
					}
				}
			}
		}

        private void BtnOK_Click(object sender, EventArgs e)
        {
            //「設定」で終了するので、結果をtrueにする。
            pDlgResult = true;
            this.Close();
        }

        private void BtnCancel_Click_1(object sender, EventArgs e)
        {
			//「キャンセル」で終了するので、結果をfalseにする。
			pDlgResult = false;
			this.Close();
		}
    }
}
