using System;

namespace LianLianKan {
    public static class LLKHelper {
        private static Array _allTokenTypes;

        public static int NumTokenTypes {
            get {
                return _allTokenTypes.Length - 1;
            }
        }

        static LLKHelper() {
            _allTokenTypes = Enum.GetValues(typeof(LLKTokenType));
        }
        public static LLKTokenType GetRandomTokenType() {
            return (LLKTokenType)_allTokenTypes.GetValue(new Random().Next(1, _allTokenTypes.Length));
        }
    }
}
