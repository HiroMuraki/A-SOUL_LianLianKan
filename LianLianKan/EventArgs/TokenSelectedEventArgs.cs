using System;

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
