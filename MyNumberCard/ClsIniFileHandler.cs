using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
using System.Threading;
using System.Reflection;

namespace PDC_230
{
	class ClsIniFileHandler
	{
		[DllImport("KERNEL32.DLL")]
		public static extern uint GetPrivateProfileString(
			  string lpAppName
			, string lpKeyName
			, string lpDefault
			, StringBuilder lpReturnedString
			, uint nSize
			, string lpFileName);

		[DllImport("KERNEL32.DLL", EntryPoint = "GetPrivateProfileStringA")]
		public static extern uint GetPrivateProfileStringByByteArray(
			  string lpAppName
			, string lpKeyName
			, string lpDefault
			, byte[] lpReturnedString
			, uint nSize
			, string lpFileName);

		[DllImport("KERNEL32.DLL")]
		public static extern uint GetPrivateProfileInt(
			string lpAppName
			, string lpKeyNam
			, int nDefault
			, string lpFileName);

		[DllImport("KERNEL32.DLL")]
		public static extern uint WritePrivateProfileString(
				string lpAppName
			, string lpKeyName
			, string lpString
			, string lpFileName);
	}
}
