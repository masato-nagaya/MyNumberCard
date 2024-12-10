using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigFile {

	using settings = Properties.Settings;
	public class ConfigSettings {
		public CodeValue CodeValue = new CodeValue();
		public WriteFormat WriteFormat = new WriteFormat();
	}


	public class CodeValue {
		public String MunicipalCode = settings.Default.MunicipalCodeValue;
		public String BranchOffice = settings.Default.BranchOfficeValue;
	}

	public class WriteFormat {
		public DataFormat MunicipalCode = new DataFormat {
												Length = (int)settings.Default.MunicipalCodeLengthValue,
												LeadingCharacter ='0'
											};
		public DataFormat BranchOffice = new DataFormat {
												Length = (int)settings.Default.BranchOfficeLengthValue,
												LeadingCharacter = '0'
											};
		public DataFormat SealRegistration = new DataFormat {
												Length = (int)settings.Default.SealRegistrationLengthValue,
												LeadingCharacter = '0'
											};
	}
	
	public class DataFormat {
		public int Length;
		public char LeadingCharacter;
	}
}
