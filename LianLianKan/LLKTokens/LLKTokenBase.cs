using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianLianKan {
    public abstract class LLKTokenBase {
        private object _content;

        public object Content {
            get {
                return _content;
            }
            set {
                _content = value;
            }
        }

        public LLKTokenBase() {

        }
    }
}
