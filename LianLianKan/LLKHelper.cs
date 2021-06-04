using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LianLianKan {
    public static class LLKHelper {
        private static readonly Array _allTokenTypes;
        private static readonly Array _allTokenCategory;
        private static readonly Random _rnd;

        public static readonly Dictionary<LLKTokenType, string> TokenResources;
        public static readonly Dictionary<TokenCategory, string> TokenCategoryThemes;
        public static readonly int NumTokenTypes;
        public static readonly int NumTokenCategory;

        static LLKHelper() {
            _rnd = new Random();
            _allTokenTypes = Enum.GetValues(typeof(LLKTokenType));
            _allTokenCategory = Enum.GetValues(typeof(TokenCategory));
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
            TokenCategoryThemes = new Dictionary<TokenCategory, string>() {
                [TokenCategory.None] = "",
                [TokenCategory.AS] = "ASTheme",
                [TokenCategory.Ava] = "AvaTheme",
                [TokenCategory.Bella] = "BellaTheme",
                [TokenCategory.Carol] = "CarolTheme",
                [TokenCategory.Diana] = "DianaTheme",
                [TokenCategory.Eileen] = "EileenTheme"
            };
            NumTokenTypes = _allTokenTypes.Length - 1;
            NumTokenCategory = _allTokenCategory.Length - 1;
        }

        public static Tuple<LLKTokenType[,], int> GenerateLayoutFrom(object obj) {
            // 当前只支持string
            if (!(obj is string)) {
                return null;
            }
            string str = (string)obj;

            var lines = str.Split('\n');
            var metaData = lines[0].Split();
            // 从第一行获取行列，技能点
            int rowSize = Convert.ToInt32(metaData[0]);
            int columnSize = Convert.ToInt32(metaData[1]);
            int skillPoint = Convert.ToInt32(metaData[2]);

            LLKTokenType[,] tokenTypes = new LLKTokenType[rowSize, columnSize];
            // 从第二行开始读取每行的元素
            for (int row = 0; row < rowSize; row++) {
                // 注意第二行读取
                var elements = lines[row + 1].Split();
                // 若有一行元素数不符，返回null
                if (elements.Count() != columnSize) {
                    return null;
                }
                for (int col = 0; col < columnSize; col++) {
                    tokenTypes[row, col] = (LLKTokenType)Convert.ToInt32(elements[col]);
                }
            }

            return Tuple.Create(tokenTypes, skillPoint);
        }
        public static string ConvertLayoutFrom(LLKTokenType[,] tokenTypes, int skillPoint) {
            int rowSize = tokenTypes.GetLength(0);
            int columnSize = tokenTypes.GetLength(1);

            StringBuilder sb = new StringBuilder();
            // 写入行、列与技能点信息
            sb.Append($"{rowSize} {columnSize} {skillPoint}\n");
            for (int row = 0; row < rowSize; row++) {
                for (int col = 0; col < columnSize; col++) {
                    // 将TokenType枚举转化为数值写入
                    sb.Append($"{(int)tokenTypes[row, col]}");
                    if (col != columnSize - 1) {
                        sb.Append(' ');
                    }
                }
                if (row != rowSize - 1) {
                    sb.Append('\n');
                }
            }

            return sb.ToString();
        }
        public static string ConvertLayoutFrom(IEnumerable<LLKTokenType> tokenTypes, int rowSize, int columnSize, int skillPoint) {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{rowSize} {columnSize} {skillPoint}\n");
            int row = 0;
            int col = 0;
            foreach (var tokenType in tokenTypes) {
                // 推入元素
                sb.Append($"{(int)tokenType}");
                // 若当前列不等于列大小，则追加一个空格
                if (col != columnSize - 1) {
                    sb.Append(' ');
                }
                // 列+1
                col += 1;
                // 若列数相等，重置列位置，行+1
                if (col == columnSize) {
                    row += 1;
                    col = 0;
                    sb.Append('\n');
                }
                if (row > rowSize) {
                    break;
                }
            }

            return sb.ToString();
        }
        public static LLKTokenType GetRandomTokenType() {
            return (LLKTokenType)_allTokenTypes.GetValue(_rnd.Next(1, _allTokenTypes.Length));
        }
        public static TokenCategory GetRandomTokenCategory() {
            return (TokenCategory)_allTokenCategory.GetValue(_rnd.Next(1, _allTokenCategory.Length));
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
