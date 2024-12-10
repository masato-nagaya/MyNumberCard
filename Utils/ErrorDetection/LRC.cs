namespace Utils.ErrorDetection {
	public class LRC {
		public static byte Calculate(byte[] data) {
			byte lrc = 0;
			for (int i = 0; i < data.Length; i++) {
				lrc ^= data[i];
			}
			return lrc;
		}
		public static byte Calculate(byte[] data,int index,int count) {
			byte lrc = 0;
			for (int i = index; i < index+count; i++) {
				lrc ^= data[i];
			}
			return lrc;
		}
	}
}
