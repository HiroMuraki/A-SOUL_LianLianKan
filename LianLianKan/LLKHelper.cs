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
        public static string GetSkillDescription(LLKSkill skill) {
            switch (skill) {
                case LLKSkill.None:
                    return "";
                case LLKSkill.AvaPower:
                    return "AVAVA!";
                case LLKSkill.BellaPower:
                    return "击穿月球!";
                case LLKSkill.CarolPower:
                    return "R I S E";
                case LLKSkill.DianaPower:
                    return "多态小草莓";
                case LLKSkill.EileenPower:
                    return "团队粘合";
                default:
                    return "";
            }
        }
    }
}
