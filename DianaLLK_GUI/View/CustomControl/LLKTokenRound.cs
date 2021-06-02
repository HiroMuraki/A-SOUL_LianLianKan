using LianLianKan;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace DianaLLK_GUI.View {
    public class LLKTokenRound : Button {
        private DoubleAnimation _animation;

        public static readonly DependencyProperty TokenProperty =
            DependencyProperty.Register(nameof(Token), typeof(LLKToken), typeof(LLKTokenRound), new PropertyMetadata(null));

        public event EventHandler<TClickEventArgs> TClick;
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
            _animation = new DoubleAnimation() {
                To = 0,
                AccelerationRatio = 0.2,
                DecelerationRatio = 0.8,
                EasingFunction = new BounceEase(),
                Duration = TimeSpan.FromMilliseconds(220)
            };
        }
        public LLKTokenRound(LLKToken token) : this() {
            Token = token;
            Token.Matched += Token_Matched;
        }

        private void Token_Matched(object sender, EventArgs e) {
            BeginAnimation(OpacityProperty, _animation);
        }

        protected override void OnClick() {
            base.OnClick();
            TClick?.Invoke(this, new TClickEventArgs(Token));
        }
    }
}
