using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Settings {
    public class Settings {
        public string AppName;
        public string Version;
        public DataFormat MunicipalCode;
        public DataFormat BranchOffice;
        public DataFormat SealRegistration;
        public DataFormat Padding;
    }

    public class DataFormat {
        public string Value;
        public int Length;
        public bool PaddingStatus;
        public char PaddingCharacter;

    }
    public enum PaddingStat {
        None,
        Left,
        Right
    }
}
