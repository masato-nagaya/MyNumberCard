using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncodeData {
	public class Settings {
		public string AppName;
		public string Version;
		public DataFormat MunicipalCode = new DataFormat();
		public DataFormat BranchOffice = new DataFormat();
		public DataFormat SealRegistration = new DataFormat();
		public DataFormat Padding = new DataFormat();
	}

	public class DataFormat {
		public string Value;
		public decimal Length;
		public PaddingStat PaddingStatus;
		public char PaddingCharacter;

	}
	public enum PaddingStat {
		None,
		Left,
		Right
	}
}
