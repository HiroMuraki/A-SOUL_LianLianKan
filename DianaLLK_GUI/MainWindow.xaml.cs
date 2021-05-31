using DianaLLK_GUI.ViewModel;
using LianLianKan;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Threading;

namespace DianaLLK_GUI {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private readonly LLKGame _game;
        private readonly GameSetter _gameSetter;
        private long _gameUsingTime;
        private DispatcherTimer _gameTimer;
        // private Point _hitPos;
        // private Line _directionLine;

        public LLKGame Game {
            get {
                return _game;
            }
        }
        public GameSetter GameSetter {
            get {
                return _gameSetter;
            }
        }

        public MainWindow() {
            // 初始化计时器
            _gameTimer = new DispatcherTimer() {
                Interval = TimeSpan.FromMilliseconds(50)
            };
            _gameTimer.Tick += GameTimer_Tick;
            //// 初始化方向线
            //_directionLine = new Line {
            //    StrokeThickness = 5,
            //    StrokeEndLineCap = PenLineCap.Triangle,
            //    StrokeDashCap = PenLineCap.Round,
            //    StrokeStartLineCap = PenLineCap.Round,
            //    Visibility = Visibility.Hidden
            //};
            // 初始化游戏设置器
            _gameSetter = new GameSetter() {
                RowSize = 6,
                ColumnSize = 10,
                TokenAmount = 15
            };
            // 初始化游戏
            _game = new LLKGame();
            _game.GameCompleted += Game_GameCompleted;
            InitializeComponent();
            GridRoot.MaxHeight = SystemParameters.WorkArea.Height;
            GridRoot.MaxWidth = SystemParameters.WorkArea.Width;
            ExpandGameSetterPanel();
            // GamePlayAreaCanvas.Children.Add(_directionLine);
        }

        private void LLKToken_Click(object sender, RoutedEventArgs e) {
            LLKToken token = (LLKToken)(sender as FrameworkElement).Tag;
            //await Task.Run(() => {
            _game.SelectToken(token);
            //});
        }
        private void Game_GameCompleted(object sender, GameCompletedEventArgs e) {
            _gameTimer.Stop();
            // 模糊背景
            BlurEffect effect = new BlurEffect();
            DoubleAnimation effectAnimation = new DoubleAnimation() {
                To = 25,
                AccelerationRatio = 0.2,
                DecelerationRatio = 0.8,
                Duration = TimeSpan.FromMilliseconds(150)
            };
            GameArea.Effect = effect;
            effect.BeginAnimation(BlurEffect.RadiusProperty, effectAnimation);
            // 弹出统计窗口
            var gameUsingTime = _gameUsingTime / 1000.0;
            var totalScores = e.TotalScores;
            View.GameCompletedWindow gcw = new View.GameCompletedWindow(e, gameUsingTime, 0, totalScores);
            gcw.Owner = this;
            gcw.ShowDialog();
            // 取消模糊背景
            GameArea.Effect = null;
            ExpandGameSetterPanel();
        }
        private void GameTimer_Tick(object sender, EventArgs e) {
            _gameUsingTime += 50;
        }

        private void ExpandGameSetter_Click(object sender, RoutedEventArgs e) {
            if (GameSetterPanel.Height != 0) {
                FoldGameSetterPanel();
            }
            else {
                ExpandGameSetterPanel();
            }
        }
        private void StartGame_Click(object sender, RoutedEventArgs e) {
            try {
                StartGame();
                FoldGameSetterPanel();
                _gameUsingTime = 0;
                _gameTimer.Start();
            }
            catch (Exception exp) {
                MessageBox.Show(exp.Message, "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //private void GameGesture_MouseUp(object sender, MouseButtonEventArgs e) {
        //    ClearDirectionLine();
        //}
        //private void GameGesture_MouseDown(object sender, MouseButtonEventArgs e) {
        //    _hitPos = e.GetPosition(GamePlayAreaCanvas);
        //}
        //private void GameGesture_MouseMove(object sender, MouseEventArgs e) {
        //    if (e.LeftButton != MouseButtonState.Pressed && e.RightButton != MouseButtonState.Pressed) {
        //        return;
        //    }
        //    Point currentPos = e.GetPosition(GamePlayAreaCanvas);
        //    // 绘制方向线
        //    DrawDirectionLine(_hitPos, currentPos);
        //}

        private void Window_Minimum(object sender, RoutedEventArgs e) {
            WindowState = WindowState.Maximized;
        }
        private void Window_Maximum(object sender, RoutedEventArgs e) {
            if (WindowState == WindowState.Maximized) {
                WindowState = WindowState.Normal;
            }
            else {
                WindowState = WindowState.Maximized;
            }
        }
        private void Window_Close(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }
        private void Window_Move(object sender, MouseButtonEventArgs e) {
            if (e.ClickCount >= 2) {
                Window_Maximum(null, null);
            }
            else {
                DragMove();
            }
        }


        /// <summary>
        /// 开始游戏
        /// </summary>
        private void StartGame() {
            _game.StartGame(_gameSetter.RowSize, _gameSetter.ColumnSize, _gameSetter.TokenAmount);
        }
        /// <summary>
        /// 展开游戏设置面板
        /// </summary>
        private void ExpandGameSetterPanel() {
            _gameSetter.OnCurrentAvatarChanged();
            DoubleAnimation heightAnimation = new DoubleAnimation() {
                To = (ActualHeight == 0 ? Height : ActualHeight) - 40,
                AccelerationRatio = 0.2,
                DecelerationRatio = 0.8,
                Duration = TimeSpan.FromMilliseconds(200)
            };
            DoubleAnimation opacityAnimation = new DoubleAnimation() {
                To = 1,
                AccelerationRatio = 0.2,
                DecelerationRatio = 0.8,
                Duration = TimeSpan.FromMilliseconds(200)
            };
            GameSetterPanel.BeginAnimation(Grid.HeightProperty, heightAnimation);
            GameSetterPanel.BeginAnimation(Grid.OpacityProperty, opacityAnimation);
        }
        /// <summary>
        /// 收起游戏设置面板
        /// </summary>
        private void FoldGameSetterPanel() {
            DoubleAnimation heightAnimation = new DoubleAnimation() {
                To = 0,
                AccelerationRatio = 0.2,
                DecelerationRatio = 0.8,
                Duration = TimeSpan.FromMilliseconds(200)
            };
            DoubleAnimation opacityAnimation = new DoubleAnimation() {
                To = 0,
                AccelerationRatio = 0.2,
                DecelerationRatio = 0.8,
                Duration = TimeSpan.FromMilliseconds(200)
            };
            GameSetterPanel.BeginAnimation(HeightProperty, heightAnimation);
            GameSetterPanel.BeginAnimation(OpacityProperty, opacityAnimation);
        }
        ///// <summary>
        ///// 计算两点距离
        ///// </summary>
        ///// <param name="startPos"></param>
        ///// <param name="endPos"></param>
        ///// <returns></returns>
        //private static double CalculateLength(Point startPos, Point endPos) {
        //    return Math.Sqrt(Math.Pow(endPos.X - startPos.X, 2) + Math.Pow(endPos.Y - startPos.Y, 2));
        //}
        ///// <summary>
        ///// 绘制方向线
        ///// </summary>
        ///// <param name="startPos"></param>
        ///// <param name="endPos"></param>
        //private void DrawDirectionLine(Point startPos, Point endPos) {
        //    _directionLine.Visibility = Visibility.Visible;
        //    // 绘制线条虚线信息
        //    double lineLength = CalculateLength(startPos, endPos);
        //    double dashLength = lineLength / 50 <= 5 ? lineLength / 50 : 5;
        //    double dashInterval = lineLength / 50 <= 3 ? lineLength / 50 : 3;
        //    _directionLine.StrokeDashArray = new DoubleCollection(new double[2] { dashLength, dashInterval });
        //    // 绘制线条颜色
        //    _directionLine.Stroke = new SolidColorBrush(Colors.White);
        //    // 绘制线条位置
        //    _directionLine.X1 = startPos.X;
        //    _directionLine.Y1 = startPos.Y;
        //    _directionLine.X2 = endPos.X;
        //    _directionLine.Y2 = endPos.Y;
        //}
        ///// <summary>
        ///// 清除方向线
        ///// </summary>
        //private void ClearDirectionLine() {
        //    _directionLine.Visibility = Visibility.Hidden;
        //    _directionLine.X1 = 0;
        //    _directionLine.Y1 = 0;
        //    _directionLine.X2 = 0;
        //    _directionLine.Y2 = 0;
        //}
    }
}