using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComText
{
	class ClsComText
	{
		private const int RingSize = 512;
		private const int BuffSize = 512;
		private int Rptr;
		private int Wptr;
		byte[,] Buff = new byte[RingSize, BuffSize];


		public ClsComText()
		{
			Rptr = 0;
			Wptr = 1;
		}

		public void SetComText(byte[] argByte, int Len)
		{

			for (int i = 0; i < Buff.GetLength(1); i++) {
				if (i < Len) {
					Buff[Wptr, i] = argByte[i];
				} else {
					Buff[Wptr, i] = 0;
				}
			}

			if (Wptr < (Buff.GetLength(0) - 1)) {
				Wptr++;
			} else {
				Wptr = 0;
			}

		}

		public byte[] GetComText()
		{
			byte[] Res = new byte[BuffSize];

			if (Rptr < (Buff.GetLength(0) - 1)) {
				Rptr++;
			} else {
				Rptr = 0;
			}

			for (int i=0; i < Buff.GetLength(0); i++) {
				Res[i] = Buff[Rptr, i];
			}
			return Res;
		}

		public int DateNum()
		{
			int tmp = Buff.GetLength(0);

			if (Wptr >= Rptr) {
				return (Wptr - Rptr) - 1;
			} else {
				return (Buff.GetLength(0) - Rptr + Wptr) - 1;
			}
		}
	}
}
