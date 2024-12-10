using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDC230Lib {
	public static class StatusCode {
		public static string ToMessage(string s) {
			string message = string.Empty;
			if (s == string.Empty) {
				return message;
			}

			switch (s) {
			case "00":
				message = Properties.Settings.Default.Status00;
				break;
			case "01":
				message = Properties.Settings.Default.Status01;
				break;
			case "11":
				message = Properties.Settings.Default.Status11;
				break;
			case "12":
				message = Properties.Settings.Default.Status12;
				break;
			case "13":
				message = Properties.Settings.Default.Status13;
				break;
			case "21":
				message = Properties.Settings.Default.Status21;
				break;
			case "22":
				message = Properties.Settings.Default.Status22;
				break;
			case "23":
				message = Properties.Settings.Default.Status23;
				break;
			case "31":
				message = Properties.Settings.Default.Status31;
				break;
			case "81":
				message = Properties.Settings.Default.Status81;
				break;
			case "82":
				message = Properties.Settings.Default.Status82;
				break;
			case "83":
				message = Properties.Settings.Default.Status83;
				break;
			case "84":
				message = Properties.Settings.Default.Status84;
				break;
			case "92":
				message = Properties.Settings.Default.Status92;
				break;
			case "93":
				message = Properties.Settings.Default.Status93;
				break;
			case "95":
				message = Properties.Settings.Default.Status95;
				break;
			case "96":
				message = Properties.Settings.Default.Status96;
				break;
			case "97":
				message = Properties.Settings.Default.Status97;
				break;
			case "98":
				message = Properties.Settings.Default.Status98;
				break;
			default:
				message = "その他のエラー";
				break;

			}
			return message;
		}


	}


}
