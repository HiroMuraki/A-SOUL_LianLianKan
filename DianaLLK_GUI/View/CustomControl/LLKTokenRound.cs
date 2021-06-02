using LianLianKan;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DianaLLK_GUI.View {
    public class LLKTokenRound : Button {
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
        public LLKTokenRound() {

        }

        protected override void OnClick() {
            base.OnClick();
            TClick?.Invoke(this, new TClickEventArgs(Token));
        }
    }
}
