using System;

namespace ConfigFile {
	public class _Settings {
		public CodeValue CodeValue = new CodeValue();
		public WriteFormat WriteFormat = new WriteFormat();
	}


	public class CodeValue {
		public String MunicipalCode = "282057";
		public String BranchOffice = "91";
	}

	public class WriteFormat {
		public DataFormat MunicipalCode = new DataFormat { Length=6, LeadingCharacter='0' };
		public DataFormat BranchOffice = new DataFormat { Length = 2, LeadingCharacter = '0' };
		public DataFormat SealRegistration = new DataFormat { Length = 14, LeadingCharacter = '0' };
	}
	
	public class DataFormat {
		public int Length;
		public char LeadingCharacter;
	}
}
