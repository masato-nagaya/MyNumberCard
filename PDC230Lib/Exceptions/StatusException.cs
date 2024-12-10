using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDC230Lib {
    class StatusException :Exception{
        public StatusException() : base(){ }
        public StatusException(string message) : base(message) { }
    }
}
