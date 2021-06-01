using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LianLianKan {
    using LLKTokens = IEnumerable<LLKToken>;
    public class LLKGame : INotifyPropertyChanged {
        private List<LLKTokenType> _currentTokenTypes;
        private LLKToken[,] _gameLayout;
        private LLKToken _heldToken;
        private int _rowSize;
        private int _columnSize;
        private int _skillPoint;
        private object _processLocker;
        private object _skillLocker;
        private bool _bellaPowerOn;
        private bool _eileenPowerOn;

        #region 公开事件
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<GameCompletedEventArgs> GameCompleted;
        public event EventHandler<SkillActivedEventArgs> SkillActived;
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
        public int SkillPoint {
            get {
                return _skillPoint;
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
            _skillLocker = new object();
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
            // 重置状态
            _bellaPowerOn = false;
            _eileenPowerOn = false;
            _heldToken = null;
            GenerateGameLayout(rowSize, columnSize, tokenAmount);
            _skillPoint = GetSkillPoint();
            OnPropertyChanged(nameof(RowSize));
            OnPropertyChanged(nameof(ColumnSize));
            OnPropertyChanged(nameof(LLKTokenArray));
            OnPropertyChanged(nameof(SkillPoint));
        }
        public async Task SelectTokenAsync(LLKToken token) {
            await Task.Run(() => {
                lock (_processLocker) {
                    SelectTokenCore(token);
                }
            });
            if (IsGameCompleted()) {
                int scores = GetTotalScores();
                GameCompleted?.Invoke(this, new GameCompletedEventArgs(scores, _currentTokenTypes.Count, _rowSize, _columnSize));
            }
        }
        public void SelectToken(LLKToken token) {
            SelectTokenCore(token);
            if (IsGameCompleted()) {
                int scores = GetTotalScores();
                GameCompleted?.Invoke(this, new GameCompletedEventArgs(scores, _currentTokenTypes.Count, _rowSize, _columnSize));
            }
        }
        public async Task ActiveSkillAsync(LLKSkill skill) {
            await Task.Run(() => {
                lock (_skillLocker) {
                    ActiveSkill(skill);
                }
            });
            SkillActived?.Invoke(this, new SkillActivedEventArgs(skill));
        }
        public void ActiveSkill(LLKSkill skill) {
            ActiveSkillCore(skill);
            SkillActived?.Invoke(this, new SkillActivedEventArgs(skill));
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

        private void SelectTokenCore(LLKToken token) {
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
                // 如果启用了贝拉Power
                if (_bellaPowerOn) {
                    if (_heldToken.TokenType == token.TokenType) {
                        MatchTokensHelper(_heldToken, token);
                        _bellaPowerOn = false;
                    }
                }
                // 常规比较
                else {
                    // 如果启用了乃琳Power
                    if (_eileenPowerOn) {
                        if (IsConnectable(_heldToken.Coordinate, token.Coordinate)) {
                            LLKTokenType fixTypeA = _heldToken.TokenType;
                            LLKTokenType fixTypeB = token.TokenType;
                            List<LLKToken> typeAList = new List<LLKToken>();
                            List<LLKToken> typeBList = new List<LLKToken>();
                            Random rnd = new Random();
                            MatchTokensHelper(_heldToken, token);
                            for (int row = 0; row < _rowSize; row++) {
                                for (int col = 0; col < _columnSize; col++) {
                                    if (_gameLayout[row, col].TokenType == fixTypeA) {
                                        typeAList.Add(_gameLayout[row, col]);
                                    }
                                    else if (_gameLayout[row, col].TokenType == fixTypeB) {
                                        typeBList.Add(_gameLayout[row, col]);
                                    }
                                }
                            }
                            LLKTokenType tType = LLKHelper.GetRandomTokenType();
                            typeAList[rnd.Next(0, typeAList.Count)].TokenType = tType;
                            typeBList[rnd.Next(0, typeBList.Count)].TokenType = tType;
                            _eileenPowerOn = false;
                        }
                    }
                    else if (IsMatchable(_heldToken.Coordinate, token.Coordinate)) {
                        MatchTokensHelper(_heldToken, token);
                    }
                }
                _heldToken.IsSelected = false;
                token.IsSelected = false;
                _heldToken = null;
            }
        }
        private void ActiveSkillCore(LLKSkill skill) {
            if (_skillPoint <= 0) {
                SkillActived?.Invoke(this, new SkillActivedEventArgs(LLKSkill.None));
                return;
            }
            switch (skill) {
                case LLKSkill.None:
                    break;
                case LLKSkill.AvaPower:
                    AvaPower();
                    break;
                case LLKSkill.BellaPower:
                    BellaPower();
                    break;
                case LLKSkill.CarolPower:
                    CarolPower();
                    break;
                case LLKSkill.DianaPower:
                    DianaPower();
                    break;
                case LLKSkill.EileenPower:
                    EileenPower();
                    break;
                default:
                    break;
            }
            OnPropertyChanged(nameof(SkillPoint));
        }
        private void MatchTokensHelper(LLKToken a, LLKToken b) {
            Coordinate aPos = a.Coordinate;
            Coordinate bPos = b.Coordinate;
            _gameLayout[aPos.Row, aPos.Column].TokenType = LLKTokenType.None;
            _gameLayout[bPos.Row, bPos.Column].TokenType = LLKTokenType.None;
            _gameLayout[aPos.Row, aPos.Column].OnMatched();
            _gameLayout[bPos.Row, bPos.Column].OnMatched();
        }
        private int GetSkillPoint() {
            int point = (CurrentTokenTypes.Count * 6) / (_rowSize * _rowSize);
            if ((CurrentTokenTypes.Count * 6) % (_rowSize * _rowSize) != 0) {
                point += 1;
            }
            return point;
        }
        private int GetTotalScores() {
            int result = 0;
            result = _rowSize * _columnSize * _currentTokenTypes.Count * 100;
            double skillPointMultiper = Math.Ceiling(Math.Log2(_skillPoint));
            if (skillPointMultiper > 1) {
                result = (int)(result * skillPointMultiper);
            }
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
        private void AvaPower() {
            if (_skillPoint < 3) {
                SkillActived?.Invoke(this, new SkillActivedEventArgs(LLKSkill.None));
                return;
            }
            Random rnd = new Random();
            for (int row = 0; row < _rowSize; row++) {
                for (int col = 0; col < _columnSize; col++) {
                    int indexB = rnd.Next(row * _columnSize + row, _rowSize * _columnSize);
                    int tRow = indexB / _columnSize;
                    int tCol = indexB % _columnSize;
                    var t = _gameLayout[row, col].TokenType;
                    _gameLayout[row, col].TokenType = _gameLayout[tRow, tCol].TokenType;
                    _gameLayout[tRow, tCol].TokenType = t;
                }
            }
            _skillPoint -= 3;
            OnPropertyChanged(nameof(LLKTokenArray));
        }
        private void BellaPower() {
            if (_skillPoint < 2) {
                SkillActived?.Invoke(this, new SkillActivedEventArgs(LLKSkill.None));
                return;
            }
            _bellaPowerOn = true;
            _skillPoint -= 2;
        }
        private void CarolPower() {
            if (_skillPoint < 1) {
                SkillActived?.Invoke(this, new SkillActivedEventArgs(LLKSkill.None));
                return;
            }
            bool canGetExtraPoint = new Random().Next(0, 2) == 0;
            if (canGetExtraPoint) {
                _skillPoint += 1;
            }
            else {
                _skillPoint -= 1;
            }
        }
        private void DianaPower() {
            if (_skillPoint < 1) {
                SkillActived?.Invoke(this, new SkillActivedEventArgs(LLKSkill.None));
                return;
            }
            Random rnd = new Random();
            List<LLKTokenType> strarwberries = new List<LLKTokenType>() {
                        LLKTokenType.D1,
                        LLKTokenType.D2,
                        LLKTokenType.D3,
                        LLKTokenType.D4,
                        LLKTokenType.D5
                    };
            List<Coordinate> strawberriesPos = new List<Coordinate>();
            for (int row = 0; row < _rowSize; row++) {
                for (int col = 0; col < _columnSize; col++) {
                    if (strarwberries.Contains(_gameLayout[row, col].TokenType)) {
                        strawberriesPos.Add(new Coordinate(row, col));
                    }
                }
            }
            while (strawberriesPos.Count > 0) {
                LLKTokenType current = strarwberries[rnd.Next(0, strarwberries.Count)];
                Coordinate a = strawberriesPos[rnd.Next(0, strawberriesPos.Count)];
                strawberriesPos.Remove(a);
                Coordinate b = strawberriesPos[rnd.Next(0, strawberriesPos.Count)];
                strawberriesPos.Remove(b);
                _gameLayout[a.Row, a.Column].TokenType = current;
                _gameLayout[b.Row, b.Column].TokenType = current;
            }
            _skillPoint -= 1;
        }
        private void EileenPower() {
            if (_skillPoint < 2) {
                SkillActived?.Invoke(this, new SkillActivedEventArgs(LLKSkill.None));
                return;
            }
            _skillPoint -= 2;
            _eileenPowerOn = true;
        }
    }
}