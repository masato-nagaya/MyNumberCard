using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDC230Lib {
	public class StatusEventArgs:EventArgs {
		public Status ChangedStatus { get; private set; }
		public int ReturnStatusCode { get; private set; }
		public string Message { get; private set; }


		public StatusEventArgs(Status stat,  string msg) {
			ChangedStatus = stat;
			Message = msg;
		}

		public StatusEventArgs(Status stat, int code,string msg) {
			ChangedStatus = stat;
			ReturnStatusCode = code;
			Message = msg;
		}

	}
}
