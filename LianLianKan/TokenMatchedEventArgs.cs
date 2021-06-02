using System;
using System.Collections.Generic;

namespace LianLianKan {
    public class TokenMatchedEventArgs : EventArgs {
        private LLKTokenType _tokenType;
        private bool _isSucess;

        public LLKTokenType TokenType {
            get {
                return _tokenType;
            }
        }
        public bool Sucess {
            get {
                return _isSucess;
            }
        }

        public TokenMatchedEventArgs(LLKTokenType matchedTokenType, bool matched) {
            _tokenType = matchedTokenType;
            _isSucess = matched;
        }
    }
}