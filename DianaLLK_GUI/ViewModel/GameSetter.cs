using LianLianKan;
using System;
using System.ComponentModel;

namespace DianaLLK_GUI.ViewModel {
    public class GameSetter : INotifyPropertyChanged {
        private static readonly Array _gameThemeList;
        private static readonly int _minSize;
        private static readonly int _maxSize;
        private static readonly int _minTokenAmount;
        private int _rowSize;
        private int _columnSize;
        private int _tokenAmount;

        public event PropertyChangedEventHandler PropertyChanged;

        public int RowSize {
            get {
                return _rowSize;
            }
            set {
                _rowSize = value;
                OnPropertyChanged(nameof(RowSize));
            }
        }
        public int ColumnSize {
            get {
                return _columnSize;
            }
            set {
                _columnSize = value;
                OnPropertyChanged(nameof(ColumnSize));
            }
        }
        public int TokenAmount {
            get {
                return _tokenAmount;
            }
            set {
                _tokenAmount = value;
                OnPropertyChanged(nameof(TokenAmount));
            }
        }
        public int MinSize {
            get {
                return _minSize;
            }
        }
        public int MaxSize {
            get {
                return _maxSize;
            }
        }
        public int MinTokenAmount {
            get {
                return _minTokenAmount;
            }
        }
        public int MaxTokenAmount {
            get {
                return LLKHelper.NumTokenTypes;
            }
        }
        public LLKTokenType CurrentAvatar {
            get {
                return GetRandomTokenType();
            }
        }

        static GameSetter() {
            _gameThemeList = Enum.GetValues(typeof(GameTheme));
            _minSize = 6;
            _maxSize = 18;
            _minTokenAmount = 4;
        }
        public GameSetter() {

        }

        public static LLKTokenType GetRandomTokenType() {
            return LLKHelper.GetRandomTokenType();
        }
        public static GameTheme GetRandomGameTheme() {
            return (GameTheme)(_gameThemeList.GetValue(new Random().Next(1, _gameThemeList.Length)));
        }
        public void OnCurrentAvatarChanged() {
            OnPropertyChanged(nameof(CurrentAvatar));
        }

        private void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
