using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace LianLianKan {
    using LLKTokens = IEnumerable<LLKToken>;
    public class LLKGame : INotifyPropertyChanged {
        private List<LLKTokenType> _currentTokenTypes;
        private LLKToken[,] _gameLayout;
        private LLKToken _heldToken;
        private int _rowSize;
        private int _columnSize;
        private object _processLocker;

        #region 公开事件
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<GameCompletedEventArgs> GameCompleted;
        #endregion

        #region 公开属性
        public List<LLKTokenType> CurrentTokenTypes {
            get {
                return _currentTokenTypes;
            }
        }
        public LLKTokens LLKTokenArray {
            get {
                for (int row = 0; row < _rowSize; row++) {
                    for (int col = 0; col < _columnSize; col++) {
                        yield return _gameLayout[row, col];
                    }
                }
            }
        }
        public int RowSize {
            get {
                return _rowSize;
            }
        }
        public int ColumnSize {
            get {
                return _columnSize;
            }
        }
        #endregion

        #region 构造方法
        public LLKGame() {
            _rowSize = 0;
            _columnSize = 0;
            _gameLayout = new LLKToken[0, 0];
            _heldToken = null;
            _processLocker = new object();
        }
        public LLKGame(string testLayoutString) {
            _gameLayout = new LLKToken[_rowSize, _columnSize];
            testLayoutString = Regex.Replace(testLayoutString, @"[\s]+", " ");
            var numberArray = testLayoutString.Split(' ');

            for (int row = 0; row < _rowSize; row++) {
                for (int col = 0; col < _columnSize; col++) {
                    int value = Convert.ToInt32(numberArray[row * _columnSize + col]);
                    _gameLayout[row, col] = new LLKToken((LLKTokenType)value);
                    _gameLayout[row, col].Coordinate = new Coordinate(row, col);
                }
            }
        }
        #endregion

        #region 公开方法
        public void StartGame(int rowSize, int columnSize, int tokenAmount) {
            GenerateGameLayout(rowSize, columnSize, tokenAmount);
            OnPropertyChanged(nameof(RowSize));
            OnPropertyChanged(nameof(ColumnSize));
            OnPropertyChanged(nameof(LLKTokenArray));
        }
        public void SelectToken(LLKToken token) {
            lock (_processLocker) {
                if (token.TokenType == LLKTokenType.None) {
                    if (_heldToken != null) {
                        _heldToken.IsSelected = false;
                    }
                    _heldToken = null;
                    return;
                }
                if (_heldToken == null) {
                    _heldToken = token;
                    _heldToken.IsSelected = true;
                }
                else {
                    MatchTokens(_heldToken.Coordinate, token.Coordinate);
                    if (IsGameCompleted()) {
                        int scores = GetTotalScores();
                        GameCompleted?.Invoke(this, new GameCompletedEventArgs(scores, _currentTokenTypes.Count, _rowSize, _columnSize));
                    }
                    _heldToken.IsSelected = false;
                    _heldToken = null;
                    token.IsSelected = false;
                }
            }
        }
        public bool MatchTokens(Coordinate a, Coordinate b) {
            if (!IsMatchable(a, b)) {
                return false;
            }
            _gameLayout[a.Row, a.Column].TokenType = LLKTokenType.None;
            _gameLayout[b.Row, b.Column].TokenType = LLKTokenType.None;
            _gameLayout[a.Row, a.Column].OnMatched();
            _gameLayout[b.Row, b.Column].OnMatched();
            return true;
        }
        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            for (int row = 0; row < _rowSize; row++) {
                for (int col = 0; col < _columnSize; col++) {
                    sb.Append($"{_gameLayout[row, col]} ");
                }
                sb.Append('\n');
            }
            return sb.ToString();
        }
        #endregion

        private int GetTotalScores() {
            int result = 0;
            result = _rowSize * _columnSize * _currentTokenTypes.Count;
            return result;
        }
        private void GenerateGameLayout(int rowSize, int columnSize, int tokenAmount) {
            // 首先随机挑选一个token，将该token生成两份
            // token添加到列表中
            // 洗牌算法打乱列表
            // 列表内容复制进游戏布局即可

            int capacity = rowSize * columnSize;
            // 如果总容量无法被2整除，则无法生成游戏
            if (capacity % 2 != 0) {
                throw new ArgumentException("行列设置无效，行列数乘积应为偶数");
            }

            Random rnd = new Random();
            // 获取可用的token类型
            List<LLKTokenType> allTokens = new List<LLKTokenType>();
            foreach (var item in Enum.GetValues(typeof(LLKTokenType))) {
                var tokenType = (LLKTokenType)item;
                if (tokenType == LLKTokenType.None) {
                    continue;
                }
                allTokens.Add(tokenType);
            }
            // 挑选token
            _currentTokenTypes = new List<LLKTokenType>();
            while (_currentTokenTypes.Count < tokenAmount) {
                if (allTokens.Count == 0) {
                    break;
                }
                var tokenType = allTokens[rnd.Next(0, allTokens.Count)];
                allTokens.Remove(tokenType);
                _currentTokenTypes.Add(tokenType);
            }
            // 随机添加token
            List<LLKTokenType> tokenTypes = new List<LLKTokenType>(capacity);
            int cycleTimes = capacity / 2;
            for (int i = 0; i < cycleTimes; i++) {
                LLKTokenType selectedType;
                // 优先各种类型至少填充一次
                if (i < tokenAmount) {
                    selectedType = _currentTokenTypes[i];
                }
                // 之后随机选择
                else {
                    selectedType = _currentTokenTypes[rnd.Next(0, _currentTokenTypes.Count)];
                }
                tokenTypes.Add(selectedType);
                tokenTypes.Add(selectedType);
            }
            // 洗牌算法打乱token顺序
            for (int i = 0; i < capacity; i++) {
                int j = rnd.Next(i, capacity);
                var t = tokenTypes[i];
                tokenTypes[i] = tokenTypes[j];
                tokenTypes[j] = t;
            }
            // 添加至游戏布局
            _rowSize = rowSize;
            _columnSize = columnSize;
            _gameLayout = new LLKToken[_rowSize, _columnSize];
            for (int row = 0; row < _rowSize; row++) {
                for (int col = 0; col < _columnSize; col++) {
                    _gameLayout[row, col] = new LLKToken(tokenTypes[row * _columnSize + col]);
                    _gameLayout[row, col].Coordinate = new Coordinate(row, col);
                }
            }
        }
        private void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private bool IsMatchable(Coordinate startCoordinate, Coordinate targetCoordinate) {
            var result = IsConnectable(startCoordinate, targetCoordinate);
            foreach (var item in _gameLayout) {
                item.IsChecked = false;
            }
            // 如果不连通，直接返回false
            if (result == false) {
                return false;
            }
            // 如果连通
            else {
                // 如果token相同，返回true
                if (LLKToken.IsSameType(_gameLayout[startCoordinate.Row, startCoordinate.Column],
                    _gameLayout[targetCoordinate.Row, targetCoordinate.Column])) {
                    return true;
                }
                // 如果连同但类型不同，返回false
                else {
                    return false;
                }
            }
        }
        private bool IsConnectable(Coordinate startCoordinate, Coordinate targetCoordinate) {
            // 先将自己标记为已检查
            _gameLayout[startCoordinate.Row, startCoordinate.Column].IsChecked = true;
            // 设置待检查列表
            List<Coordinate> checkList = new List<Coordinate>();
            for (int i = -1; i < 2; i++) {
                for (int j = -1; j < 2; j++) {
                    // 获取新位置
                    int nRow = startCoordinate.Row + i;
                    int nCol = startCoordinate.Column + j;
                    Coordinate nCoordinate = new Coordinate(nRow, nCol);
                    // 跳过四角
                    if ((i == -1 || i == 1) && (j == -1 || j == 1)) {
                        continue;
                    }
                    // 跳过中心
                    if (i == 0 && j == 0) {
                        continue;
                    }
                    // 跳过负位置
                    if (nRow < 0 || nCol < 0) {
                        continue;
                    }
                    // 跳过越界位置
                    if (nRow >= _rowSize || nCol >= _columnSize) {
                        continue;
                    }
                    // 跳过已检查
                    if (_gameLayout[nRow, nCol].IsChecked) {
                        continue;
                    }
                    checkList.Add(nCoordinate);
                }
            }
            // 开始检查
            foreach (var coordinate in checkList) {
                // 检查该位置是否为目标位置，是立即返回True
                if (coordinate == targetCoordinate) {
                    _gameLayout[coordinate.Row, coordinate.Column].IsChecked = true;
                    return true;
                }
                // 否则如果该位置是空的话，则递归检查
                else if (_gameLayout[coordinate.Row, coordinate.Column].TokenType == LLKTokenType.None) {
                    bool result = IsConnectable(coordinate, targetCoordinate);
                    // 递归检查结果为true，直接返回
                    if (result == true) {
                        return result;
                    }
                    // 否则如果待查列表中还存在未检查坐标，则跳过
                    bool allChecked = true;
                    foreach (var item in checkList) {
                        if (_gameLayout[item.Row, item.Column].IsChecked == false) {
                            allChecked = false;
                            break;
                        }
                    }
                    // 否则若所有坐标都检查完毕，返回false
                    if (allChecked) {
                        return result;
                    }
                }
                // 否则跳过此方块
                else {
                    continue;
                }
            }
            // 若检查完毕后到了这里，说明没有直接连同
            return false;
        }
        private bool IsGameCompleted() {
            foreach (var token in _gameLayout) {
                if (token.TokenType != LLKTokenType.None) {
                    return false;
                }
            }
            return true;
        }
    }
}