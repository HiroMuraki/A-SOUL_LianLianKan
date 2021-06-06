using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianLianKan {
    public class TokenSelectedEventArgs : EventArgs {
        private LLKTokenType _selectedType;

        public LLKTokenType MyProperty {
            get {
                return _selectedType;
            }
        }

        public TokenSelectedEventArgs(LLKTokenType tokenType) {
            _selectedType = tokenType;
        }

    }
}
