using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.ComponentModel;
using Utils.ErrorDetection;
using Utils.IO;
using System.Threading;
using System.Linq;

namespace PDC230Lib {

	public class PDC230 : SerialPort,IDisposable {
		        
        public static string[] GePortNames() {

			List<string> ftdi = Ports.GetPortNames("0626", "1002");
			string[] activ = SerialPort.GetPortNames();
			List<string> ports = new List<string>();

			foreach (var p1 in ftdi)
			{
				foreach (var p2 in activ)
				{
					if (p1 == p2)
					{
						ports.Add(p1);
					}
				}
			}
			return ports.ToArray();

		}

		private StatusEventArgs _StatusEventArgs = new StatusEventArgs(Status.Idel,0,string.Empty);
		public event EventHandler StatusChanged;
		public void OnStatusChanged(StatusEventArgs e) {
			if (StatusChanged != null) {
				StatusChanged(this, e);
			}
		}



		#region コンストラクタ
		public PDC230() : base() { }
		public PDC230(IContainer container) : base(container) { }
		public PDC230(string portName) : base(portName) { }
		public PDC230(string portName, int baudRate) : base(portName, baudRate) { }
		public PDC230(string portName, int baudRate, Parity parity) : base(portName, baudRate, parity) { }
		public PDC230(string portName, int baudRate, Parity parity, int dataBits) : base(portName, baudRate, parity, dataBits) { }
		public PDC230(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits) : base(portName, baudRate, parity, dataBits, stopBits) { }
		#endregion

		
		//public new void Dispose() {
		//	if (_DataQueue != null) {
		//		_DataQueue = null;
		//	}
		//	if (_StatusQueue != null) {
		//		_StatusQueue = null;
		//	}
	
		//	base.Dispose(true);
		//}

		private Response CommandWrite(byte[] cmd) {
			base.Write(cmd, 0, cmd.Length);
			Response resp = CommnandResponse();
			return resp;
		}

		private Response CommnandResponse() {
			Response resp;
			this.ReadTimeout = 1000;
			try {
				for (;;) {
					int d = base.ReadByte();
					if (d == 0x06) { resp = Response.ACK; break; }
					// if (d == 0x15) { resp = Response.NAK; break; }
					if (d == 0x15) {
						throw new CommunicationException("NAK Received.");
					}
				}
			}
			//catch (TimeoutException) { resp = Response.TimeOut; }
			catch (TimeoutException) {
				throw new CommunicationException("Receive Timeout.");
			}
			finally {
				base.ReadTimeout = 0;
			}
			return resp;
		}

		public void StatusResponse(byte[] buffer) {
			base.ReadTimeout = 10000;
			int lrc = 0;            // LRC
			int i = 0;              // index
			int d = 0;              // data
			try {
				bool stx = false;
				while (true) {
					d = base.ReadByte();
					if (stx == false) {
						if (d == 0x02) {
							stx = true;
						}
						continue;
					}
					lrc ^= d;
					if (d == 0x03) { break; }
					buffer[i] = (byte)d;
					i++;
				}
				d = base.ReadByte();
				if (d != lrc) {
					throw new Exception("Longitudinal Redundancy Check Error");
				}
			}
			//catch (TimeoutException) {
			//	throw new CommunicationException("Receive Timeout.");
			//}
			finally { this.ReadTimeout = 0; }
		}

		// 磁気データMax
		private const int MagDataMax = 69;

		// 単体コマンド
		private readonly byte[] COMMAND_I = { 0x02, 0x49, 0x03, 0x4a };
		private readonly byte[] COMMAND_E = { 0x02, 0x45, 0x03, 0x46 };
		private readonly byte[] COMMAND_F = { 0x02, 0x46, 0x03, 0x45 };
		private readonly byte[] COMMAND_B = { 0x02, 0x42, 0x03, 0x41 };
		private readonly byte[] COMMAND_A = { 0x02, 0x41, 0x03, 0x42 };

		private readonly byte[] COMMAND_C = { 0x02, 0x43, 0x03, 0x40 };
		private readonly byte[] COMMAND_R = { 0x02, 0x52, 0x03, 0x51 };
		private readonly byte[] COMMAND_O = { 0x02, 0x4f, 0x03, 0x4c };
		private readonly byte[] COMMAND_V = { 0x02, 0x56, 0x03, 0x55 };

		private Command _Command = Command.R;
		private Status _Status = Status.Idel;
		
		public Status Status {
			get { return _Status;  }
		}

		private void ChangeStatus() {

		}

		public void Cancel() {
			base.DataReceived -= new SerialDataReceivedEventHandler(this.PDC230_DataReceived);
			CommandWrite(COMMAND_C);
			Thread.Sleep(200);

			base.DiscardInBuffer();
			_DataQueue.Clear();
			_StatusQueue.Clear();

			_Command = Command.None;
		}
		// Status なし
		/// <summary>
		/// カード排出要求
		/// </summary>
		public void Eject() {
			CommandWrite(COMMAND_E);
		}

		/// <summary>
		/// カード強制排出要求
		/// </summary>
		public void ForceEject() {
			CommandWrite(COMMAND_F);
		}
		/// <summary>
		/// カード後方排出要求
		/// </summary>
		public void BackEject() {
			CommandWrite(COMMAND_B);
		}
		/// <summary>
		/// LED・ブザー自動モード要求
		/// </summary>
		public void AutoIndicatorMode() {
			CommandWrite(COMMAND_A);
		}

		/// <summary>
		/// 機器バージョン取得
		/// </summary>
		/// <returns>バージョン情報</returns>
		public string GetVersion() {
			CommandWrite(COMMAND_V);
			byte[] buffer = new byte[256];
			StatusResponse(buffer);
			string s = Encoding.ASCII.GetString(buffer);
			return s.Trim('\0');
		}

		public void CardReadContinue() {
			CommandWrite(COMMAND_R);
			_Command = Command.R;
			RecvBuffClr();
			base.DataReceived += new SerialDataReceivedEventHandler(this.PDC230_DataReceived);
		}

		public void CardRead() {
			CommandWrite(COMMAND_O);
			_Command = Command.O;
			RecvBuffClr();
			base.DataReceived += new SerialDataReceivedEventHandler(this.PDC230_DataReceived);

		}

		public void CardWrite(string s, char c) {
			CardWrite(s.PadRight(MagDataMax, c));
		}

		public void CardWrite(string s) {
			byte[] data = Encoding.ASCII.GetBytes("\x02\x77" + s + "\x03\x00");
			data[data.Length - 1] = LRC.Calculate(data, 1, data.Length - 1);

			CommandWrite(data);
			_Command = Command.w;
			RecvBuffClr();
			base.DataReceived += new SerialDataReceivedEventHandler(this.PDC230_DataReceived);
		}

		public void CardWriteHold(string s,char c) {
			CardWriteHold(s.PadRight(MagDataMax, c));
		}

		public void CardWriteHold(string s) {
			byte[] data = Encoding.ASCII.GetBytes("\x02\x57" + s + "\x03\x00");
			data[data.Length - 1] = LRC.Calculate(data, 1, data.Length - 1);

			CommandWrite(data);
			_Command = Command.W;
			RecvBuffClr();
			base.DataReceived += new SerialDataReceivedEventHandler(this.PDC230_DataReceived);
		}


		/// <summary>
		/// カード挿入要求
		/// </summary>
		public void Insert() {
			CommandWrite(COMMAND_I);
			_Command = Command.I;
			RecvBuffClr();
			this.DataReceived += new SerialDataReceivedEventHandler(this.PDC230_DataReceived);
		}

		public void Indicator() {
			_Command = Command.I;
			throw new NotImplementedException();
		}

		public void DataMode() {
			_Command = Command.M;
			throw new NotImplementedException();
		}

		public string GetData() {
			string s = string.Empty;
			if (0 != _DataQueue.Count) {
				s = _DataQueue.Dequeue().TrimStart('D');
			}
			return s;
		}
		public string GetStatus() {
			string s = string.Empty;
			if (0 != _StatusQueue.Count) {
				s = _StatusQueue.Dequeue().TrimStart('S');
			}
			return s;
		}

		private byte[] _RecvBuff = new byte[256];
		private int _Index = 0;
		private bool _Stx = false;
		private bool _Etx = false;
		private int _Lrc = 0;
		private Queue<string> _DataQueue = new Queue<string>();
		private Queue<string> _StatusQueue = new Queue<string>();

		private void RecvBuffClr() {
			Array.Clear(_RecvBuff, 0, _RecvBuff.Length);
			_Index = 0;
			_Stx = false;
			_Etx = false;
			_Lrc = 0;

		}
		private void PDC230_DataReceived(object sender, SerialDataReceivedEventArgs e) {
			int d = 0;
			if (_Etx == false) {
				while (true) {
					if (0 == base.BytesToRead) { return; }
					d = base.ReadByte();
					if (_Stx == false) {
						if (d == 0x02) {
							_Stx = true;
							_Index = 0;
							_Lrc = 0;
						}
						continue;
					}
					_Lrc ^= d;
					if (d == 0x03) { _Etx = true; break; }
					_RecvBuff[_Index] = (byte)d;
					_Index++;
				}
			}
			if (0 == base.BytesToRead) { return; }
			d = base.ReadByte();
			_Stx = false;
			_Etx = false;
			if (d != _Lrc) {
				throw new Exception("Longitudinal Redundancy Check Error");
			}
			StatusEventArgs arg;
			switch ((char)_RecvBuff[0]) {
			case 'D':
				_DataQueue.Enqueue(Encoding.ASCII.GetString(_RecvBuff).Trim('\0'));
				RecvBuffClr();

				arg = new StatusEventArgs(Status.DataRead, string.Empty);
				OnStatusChanged(arg);
				break;
			case 'S':
				_StatusQueue.Enqueue(Encoding.ASCII.GetString(_RecvBuff).Trim('\0'));
				RecvBuffClr();

				arg = new StatusEventArgs(Status.StatusRead, string.Empty);
				OnStatusChanged(arg);
				break;
			default:
				throw new Exception("Illegal packet.");
			}
			switch (_Command) {
			case Command.I:
			case Command.O:
			case Command.W:
			case Command.w:
				base.DataReceived -= new SerialDataReceivedEventHandler(this.PDC230_DataReceived);
				_Command = Command.None;
				break;
			}

		}

	}
}