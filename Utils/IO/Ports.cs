using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Utils.IO {
	public class Ports {

		public static List<string> GetPortNames(string vid, string pid) {
			String pattern = String.Format("^VID_{0}.PID_{1}", vid, pid);
			Regex _rx = new Regex(pattern, RegexOptions.IgnoreCase);
			List<string> comports = new List<string>();
			RegistryKey rk1 = Registry.LocalMachine;
			RegistryKey rk2 = rk1.OpenSubKey("SYSTEM\\CurrentControlSet\\Enum");
			foreach (String s3 in rk2.GetSubKeyNames()) {
				RegistryKey rk3 = rk2.OpenSubKey(s3);
				foreach (String s in rk3.GetSubKeyNames()) {
					if (_rx.Match(s).Success) {
						RegistryKey rk4 = rk3.OpenSubKey(s);
						foreach (String s2 in rk4.GetSubKeyNames()) {
							RegistryKey rk5 = rk4.OpenSubKey(s2);
							RegistryKey rk6 = rk5.OpenSubKey("Device Parameters");
							comports.Add((string)rk6.GetValue("PortName"));
						}
					}
				}
			}
			return comports;
		}
	}
}
