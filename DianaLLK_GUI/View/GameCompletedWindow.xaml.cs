// using MergeDiana.GameLib;
using LianLianKan;
using System.Windows;
using System.Windows.Input;

namespace DianaLLK_GUI.View {
    /// <summary>
    /// GameCompletedWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GameCompletedWindow : Window {
        private double _gameUsingTime;
        private int _tokenAmount;
        private string _gameSize;
        private int _skillActivedTimes;
        private int _totalScores;
        private LLKTokenType _tokenType;

        public LLKTokenType TokenType {
            get {
                return _tokenType;
            }
        }
        public int TokenAmount {
            get {
                return _tokenAmount;
            }
        }
        public double GameUsingTime {
            get {
                return _gameUsingTime;
            }
        }
        public int SkillActivedTimes {
            get {
                return _skillActivedTimes;
            }
        }
        public int TotalScores {
            get {
                return _totalScores;
            }
        }
        public string GameSize {
            get {
                return _gameSize;
            }
        }

        public GameCompletedWindow(GameCompletedEventArgs e, double gameUsingTime, int skillActivedTimes, int totalScore) {
            _tokenAmount = e.TokenAmount;
            _gameUsingTime = gameUsingTime;
            _skillActivedTimes = skillActivedTimes;
            _totalScores = totalScore;
            _gameSize = $"{e.RowSize} x {e.ColumnSize}";
            _tokenType = ViewModel.GameSetter.GetRandomTokenType();
            InitializeComponent();
        }

        private void Window_Close(object sender, RoutedEventArgs e) {
            Close();
        }
        private void Window_Move(object sender, MouseButtonEventArgs e) {
            DragMove();
        }
    }
}
