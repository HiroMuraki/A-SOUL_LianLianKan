using LianLianKan;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace DianaLLK_GUI.View {
    public class LLKTokenRound : Button {
        private readonly DoubleAnimation _flickAnimation;
        private readonly DoubleAnimation _selectedAnimation;
        private readonly DoubleAnimation _hoveredAnimation;
        private readonly DoubleAnimation _resetAnimation;

        public static readonly DependencyProperty SelectedHighlighterOpacityProperty =
            DependencyProperty.Register(nameof(SelectedHighlighterOpacity), typeof(double), typeof(LLKTokenRound), new PropertyMetadata(0.0));
        public static readonly DependencyProperty HoveredHightliterOpacityProperty =
            DependencyProperty.Register(nameof(HoveredHightliterOpacity), typeof(double), typeof(LLKTokenRound), new PropertyMetadata(0.0));
        public static readonly DependencyProperty TokenProperty =
            DependencyProperty.Register(nameof(Token), typeof(LLKToken), typeof(LLKTokenRound), new PropertyMetadata(null));

        public event EventHandler<TClickEventArgs> TClick;
        public double SelectedHighlighterOpacity {
            get {
                return (double)GetValue(SelectedHighlighterOpacityProperty);
            }
            set {
                SetValue(SelectedHighlighterOpacityProperty, value);
            }
        }
        public double HoveredHightliterOpacity {
            get {
                return (double)GetValue(HoveredHightliterOpacityProperty);
            }
            set {
                SetValue(HoveredHightliterOpacityProperty, value);
            }
        }
        public LLKToken Token {
            get {
                return (LLKToken)GetValue(TokenProperty);
            }
            set {
                SetValue(TokenProperty, value);
            }
        }

        static LLKTokenRound() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LLKTokenRound), new FrameworkPropertyMetadata(typeof(LLKTokenRound)));
        }
        private LLKTokenRound() {
            _flickAnimation = new DoubleAnimation() {
                To = 0,
                AccelerationRatio = 0.2,
                DecelerationRatio = 0.8,
                EasingFunction = new BounceEase(),
                Duration = TimeSpan.FromMilliseconds(200)
            };
            _hoveredAnimation = new DoubleAnimation() {
                To = 0.2,
                AccelerationRatio = 0.2,
                DecelerationRatio = 0.8,
                Duration = TimeSpan.FromMilliseconds(50)
            };
            _selectedAnimation = new DoubleAnimation() {
                To = 0.75,
                AccelerationRatio = 0.2,
                DecelerationRatio = 0.8,
                Duration = TimeSpan.FromMilliseconds(100)
            };
            _resetAnimation = new DoubleAnimation() {
                To = 0,
                AccelerationRatio = 0.2,
                DecelerationRatio = 0.8,
                Duration = TimeSpan.FromMilliseconds(100)
            };
        }
        public LLKTokenRound(LLKToken token) : this() {
            Token = token;
            Token.Selected += Token_Selected;
            Token.Matched += Token_Matched;
            Token.Reseted += Token_Reseted;
        }

        private void Token_Selected(object sender, EventArgs e) {
            base.OnClick();
            BeginAnimation(SelectedHighlighterOpacityProperty, _selectedAnimation);
            TClick?.Invoke(this, new TClickEventArgs(Token));
        }
        private void Token_Reseted(object sender, EventArgs e) {
            BeginAnimation(SelectedHighlighterOpacityProperty, _resetAnimation);
            BeginAnimation(HoveredHightliterOpacityProperty, _resetAnimation);
        }
        private void Token_Matched(object sender, EventArgs e) {
            BeginAnimation(OpacityProperty, _flickAnimation);
        }
        protected override void OnClick() {
            base.OnClick();
            TClick?.Invoke(this, new TClickEventArgs(Token));
        }
        protected override void OnMouseEnter(MouseEventArgs e) {
            base.OnMouseEnter(e);
            BeginAnimation(HoveredHightliterOpacityProperty, _hoveredAnimation);
        }
        protected override void OnMouseLeave(MouseEventArgs e) {
            base.OnMouseLeave(e);
            BeginAnimation(HoveredHightliterOpacityProperty, _resetAnimation);
        }
    }
}
