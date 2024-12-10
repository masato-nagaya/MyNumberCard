using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDC_230
{
	class ClsStatusMessage
	{
		private static int MsgNum = 30;
		private string[] StrMsg = new string[MsgNum];
		private int[] iCode = new int[MsgNum];
		private int CodeNo = -1;

		public ClsStatusMessage()
		{
			iCode[0] = 0; StrMsg[0] = "コマンドは正常に終了しました。";
			iCode[1] = 1; StrMsg[1] = "リーダーライターが再スタートしました。";
			iCode[2] = 11; StrMsg[2] = "データが読み取れません。カードの挿入方向は合っていますか？";
			iCode[3] = 12; StrMsg[3] = "磁気カード読み取りエラー。";
			iCode[4] = 13; StrMsg[4] = "銀行系または信販系のカードは読み出せません。";
			iCode[5] = 21; StrMsg[5] = "データが書き込めません。カードの挿入方向は合っていますか？";
			iCode[6] = 22; StrMsg[6] = "書き込みデータ読み出し照合エラー。";
			iCode[7] = 23; StrMsg[7] = "銀行系または信販系のカードで使われるコードを書き込もうとしました。";
			iCode[8] = 91; StrMsg[8] = "カードセンサー（挿入口）が正常に反応しません。";
			iCode[9] = 92; StrMsg[9] = "カードセンサー（磁気ヘッド位置）が正常に反応しません。";
			iCode[10] = 93; StrMsg[10] = "カードセンサー（ユニット奥）が正常に反応しません。";
			iCode[11] = 94; StrMsg[11] = "モーター制御が正しく行えません。";
			iCode[12] = 95; StrMsg[12] = "カードを抜いてもう一度挿入してください。";
			iCode[13] = 96; StrMsg[13] = "カード位置が異常です。一度強制排出してください。";
			iCode[14] = 97; StrMsg[14] = "長すぎるカードです。";
			iCode[15] = 98; StrMsg[15] = "短すぎるカードです。";
			iCode[19] = 99; StrMsg[19] = "カードが詰まりました。";

			iCode[20] = 10; StrMsg[20] = "通信タイムアウトが発生しました。";


			iCode[MsgNum - 1] = -1; StrMsg[MsgNum - 1] = "未定義エラー発生。";
		}

		public int Code
		{
			set
			{
				this.CodeNo = value;
			}
			get
			{
				return this.CodeNo;
			}

		}
		public string Message
		{
			get
			{
				string strRes = StrMsg[MsgNum - 1];
				for (int i = 0; i < iCode.Length; i++) {
					if (CodeNo == iCode[i]) {
						strRes = StrMsg[i];
						break;
					}
				}
				strRes = "CODE " + CodeNo.ToString() + '\n' + strRes;
				return strRes;
			}
		}
	}
}