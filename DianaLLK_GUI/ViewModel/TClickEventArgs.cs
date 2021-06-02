using LianLianKan;
using System;

namespace DianaLLK_GUI {
    public class TClickEventArgs : EventArgs {
        private LLKToken _token;
        public LLKToken Token {
            get {
                return _token;
            }
        }
        public TClickEventArgs(LLKToken token) {
            _token = token;
        }
    }
}
