using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDC230Lib.Events {
	public class StatusChange {
		public event EventHandler StatusChangeEvent;
		public void OnStatusChange() { }
	}
}
