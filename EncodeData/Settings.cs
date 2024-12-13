using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncodeData {
	public class Settings {
		public string AppName;
		public string Version;
		public DataFormat StartPosition = new DataFormat();     //開始位置			//add 2024/12/11
		public DataFormat MunicipalCode = new DataFormat();		//ヘッダー
		public DataFormat BranchOffice = new DataFormat();      //拠点コード
		public DataFormat SealRegistration = new DataFormat();	//印鑑登録書番号
		public DataFormat PaddingLeft = new DataFormat();       //左詰め			//add 2024/12/11	
		public DataFormat Padding = new DataFormat();           //右詰め

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
		Right,
		ON      //add 2024/12/11
	}
}
