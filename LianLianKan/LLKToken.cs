using System;
using System.ComponentModel;

namespace LianLianKan {
    public class LLKToken : INotifyPropertyChanged {
        private bool _isChecked;
        private LLKTokenType _tokenType;
        private Coordinate _coordinate;
        private bool _isSelected;

        #region 公开事件
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler Matched;
        #endregion

        #region 公开属性
        public bool IsChecked {
            get {
                return _isChecked;
            }
            set {
                _isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
        }
        public LLKTokenType TokenType {
            get {
                return _tokenType;
            }
            set {
                _tokenType = value;
                OnPropertyChanged(nameof(TokenType));
            }
        }
        public Coordinate Coordinate {
            get {
                return _coordinate;
            }
            set {
                _coordinate = value;
            }
        }
        public bool IsSelected {
            get {
                return _isSelected;
            }
            set {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }
        #endregion

        #region 构造方法
        public LLKToken(LLKTokenType content) {
            _tokenType = content;
            _isChecked = false;
            _isSelected = false;
        }
        #endregion

        #region 公开方法
        public void OnMatched() {
            Matched?.Invoke(this, new EventArgs());
        }
        public static bool IsSameType(LLKToken left, LLKToken right) {
            return left._tokenType == right._tokenType;
        }
        public override string ToString() {
            if (_tokenType == LLKTokenType.None) {
                return "?";
            }
            return _tokenType.ToString();
        }
        #endregion

        private void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}