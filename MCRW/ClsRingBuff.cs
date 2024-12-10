using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RingBuff
{
	class ClsRingBuff
	{
		private int Rptr;
		private int Wptr;
		byte[] ReadBuff = new byte[512];


		public ClsRingBuff()
		{
			Rptr = 0;
			Wptr = 1;
		}

		public void SetRecBuff(byte[] argByte, int Len)
		{
			for (int i = 0; i < Len; i++) {
				ReadBuff[Wptr] = argByte[i];

				if (Wptr < (ReadBuff.Length - 1)) {
					Wptr++;
				} else {
					Wptr = 0;
				}
			}

		}
		public byte GetRecBuff()
		{
			byte Res;

			if (Rptr < (ReadBuff.Length - 1)) {
				Rptr++;
			} else {
				Rptr = 0;
			}
			Res = ReadBuff[Rptr];

			return Res;
		}
		public int DateNum()
		{
			if (Wptr >= Rptr) {
				return (Wptr - Rptr)- 1;
			} else {
				return (ReadBuff.Length - Rptr + Wptr)- 1;
			}
		}
	}
}
