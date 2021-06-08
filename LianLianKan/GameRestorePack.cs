using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace LianLianKan {
    public record GameRestorePack {
        private readonly int _rowSize;
        private readonly int _columnSize;
        private readonly int _skillPoint;
        private readonly int _tokenAmount;
        private readonly LLKTokenType[,] _tokenTypes;

        public LLKTokenType[,] TokenTypes {
            get {
                return _tokenTypes;
            }
        }
        public int SkillPoint {
            get {
                return _skillPoint;
            }
        }
        public int ColumnSize {
            get {
                return _columnSize;
            }
        }
        public int RowSize {
            get {
                return _rowSize;
            }
        }
        public int TokenAmount {
            get {
                return _tokenAmount;
            }
        }

        public GameRestorePack(LLKTokenType[,] tokenTypes, int tokenAmount, int skillPoint) {
            _rowSize = tokenTypes.GetLength(0);
            _columnSize = tokenTypes.GetLength(1);
            _tokenTypes = tokenTypes;
            _tokenAmount = tokenAmount;
            _skillPoint = skillPoint;
        }

        public static GameRestorePack GenerateGameInfoFrom(object obj) {
            // 当前只支持string
            if (!(obj is string)) {
                return null;
            }
            string str = (string)obj;

            var lines = Regex.Split(str, @"\n");
            var metaData = Regex.Split(lines[0], @"[\s]+");
            // 从第一行获取行列，成员类数和技能点信息
            int rowSize = Convert.ToInt32(metaData[0]);
            int columnSize = Convert.ToInt32(metaData[1]);
            int numTokenTypes = Convert.ToInt32(metaData[2]);
            int skillPoint = Convert.ToInt32(metaData[3]);
            LLKTokenType[,] tokenTypes = new LLKTokenType[rowSize, columnSize];
            // 从第二行开始读取每行的元素
            for (int row = 0; row < rowSize; row++) {
                // 注意应该从第二行读取
                var elements = Regex.Split(lines[row + 1], @"[\s]+");
                // 若有一行元素数不符合列数，返回null
                if (elements.Length != columnSize) {
                    return null;
                }
                for (int col = 0; col < columnSize; col++) {
                    tokenTypes[row, col] = (LLKTokenType)Convert.ToInt32(elements[col]);
                }
            }

            return new GameRestorePack(tokenTypes, numTokenTypes, skillPoint);
        }
        public static string GetGameInfoFrom(LLKTokenType[,] tokenTypes, int tokenAmount, int skillPoint) {
            StringBuilder sb = new StringBuilder();
            int rowSize = tokenTypes.GetLength(0);
            int columnSize = tokenTypes.GetLength(1);
            sb.Append(GenerateMetaDataString(rowSize, columnSize, tokenAmount, skillPoint));
            sb.Append(GenerateLayoutString(tokenTypes));
            return sb.ToString();
        }
        public static string GetGameInfoFrom(IEnumerable<LLKTokenType> tokenTypes, int rowSize, int columnSize, int numTokenTypes, int skillPoint) {
            StringBuilder sb = new StringBuilder();
            sb.Append(GenerateMetaDataString(rowSize, columnSize, numTokenTypes, skillPoint));
            sb.Append(GenerateLayoutString(tokenTypes, rowSize, columnSize));
            return sb.ToString();
        }
        private static string GenerateMetaDataString(int rowSize, int columnSize, int tokenAmount, int skillPoint) {
            return $"{rowSize} {columnSize} {tokenAmount} {skillPoint}\n";
        }
        private static string GenerateLayoutString(IEnumerable<LLKTokenType> tokenTypes, int rowSize, int columnSize) {
            StringBuilder sb = new StringBuilder(rowSize * columnSize * 2);
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
        private static string GenerateLayoutString(LLKTokenType[,] tokenTypes) {
            int rowSize = tokenTypes.GetLength(0);
            int columnSize = tokenTypes.GetLength(1);
            StringBuilder sb = new StringBuilder(rowSize * columnSize * 2);
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
    }
}
