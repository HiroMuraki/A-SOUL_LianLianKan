using System;
using System.Collections.Generic;

namespace LianLianKan {
    public static class LLKHelper {
        private static Array _allTokenTypes;
        public readonly static Dictionary<LLKTokenType, string> TokenResources;
        public static int NumTokenTypes {
            get {
                return _allTokenTypes.Length - 1;
            }
        }

        static LLKHelper() {
            _allTokenTypes = Enum.GetValues(typeof(LLKTokenType));
            TokenResources = new Dictionary<LLKTokenType, string>() {
                [LLKTokenType.None] = null,

                [LLKTokenType.AS] = "AS",

                [LLKTokenType.A1] = "A1",
                [LLKTokenType.A2] = "A2",
                [LLKTokenType.A3] = "A3",
                [LLKTokenType.A4] = "A4",
                [LLKTokenType.A5] = "A5",

                [LLKTokenType.B1] = "B1",
                [LLKTokenType.B2] = "B2",
                [LLKTokenType.B3] = "B3",
                [LLKTokenType.B4] = "B4",
                [LLKTokenType.B5] = "B5",

                [LLKTokenType.C1] = "C1",
                [LLKTokenType.C2] = "C2",
                [LLKTokenType.C3] = "C3",
                [LLKTokenType.C4] = "C4",
                [LLKTokenType.C5] = "C5",

                [LLKTokenType.D1] = "D1",
                [LLKTokenType.D2] = "D2",
                [LLKTokenType.D3] = "D3",
                [LLKTokenType.D4] = "D4",
                [LLKTokenType.D5] = "D5",

                [LLKTokenType.E1] = "E1",
                [LLKTokenType.E2] = "E2",
                [LLKTokenType.E3] = "E3",
                [LLKTokenType.E4] = "E4",
                [LLKTokenType.E5] = "E5"
            };
        }

        public static LLKTokenType GetRandomTokenType() {
            return (LLKTokenType)_allTokenTypes.GetValue(new Random().Next(1, _allTokenTypes.Length));
        }
        public static TokenCategory GetTokenCategoryFromTokenType(LLKTokenType tokenType) {
            switch (tokenType) {
                case LLKTokenType.None:
                    return TokenCategory.None;
                case LLKTokenType.AS:
                    return TokenCategory.AS;
                case LLKTokenType.A1:
                case LLKTokenType.A2:
                case LLKTokenType.A3:
                case LLKTokenType.A4:
                case LLKTokenType.A5:
                    return TokenCategory.Ava;
                case LLKTokenType.B1:
                case LLKTokenType.B2:
                case LLKTokenType.B3:
                case LLKTokenType.B4:
                case LLKTokenType.B5:
                    return TokenCategory.Bella;
                case LLKTokenType.C1:
                case LLKTokenType.C2:
                case LLKTokenType.C3:
                case LLKTokenType.C4:
                case LLKTokenType.C5:
                    return TokenCategory.Carol;
                case LLKTokenType.D1:
                case LLKTokenType.D2:
                case LLKTokenType.D3:
                case LLKTokenType.D4:
                case LLKTokenType.D5:
                    return TokenCategory.Diana;
                case LLKTokenType.E1:
                case LLKTokenType.E2:
                case LLKTokenType.E3:
                case LLKTokenType.E4:
                case LLKTokenType.E5:
                    return TokenCategory.Eileen;
                default:
                    return TokenCategory.None;
            }
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
