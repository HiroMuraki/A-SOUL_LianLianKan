using System;

namespace LianLianKan {
    public class GameCompletedEventArgs : EventArgs {
        private int _totalScores;
        private int _tokenAmount;
        private int _rowSize;
        private int _columnSize;
        private GameType _gameType;

        public int TotalScores {
            get {
                return _totalScores;
            }
        }
        public int TokenAmount {
            get {
                return _tokenAmount;
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
        public GameType GameType {
            get {
                return _gameType;
            }
        }

        public GameCompletedEventArgs(int totalScores, int tokenAmount, int rowSize, int columnSize, GameType gameType) {
            _totalScores = totalScores;
            _tokenAmount = tokenAmount;
            _rowSize = rowSize;
            _columnSize = columnSize;
            _gameType = gameType;
        }

    }
}
