using LianLianKan;
using System.Windows;
using System.Windows.Controls;

namespace DianaLLK_GUI.View {
    public class LLKTokenRound : Button {
        public static readonly DependencyProperty TokenTypeProperty =
            DependencyProperty.Register(nameof(TokenType), typeof(LLKTokenType), typeof(LLKTokenRound), new PropertyMetadata(LLKTokenType.None));
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(LLKTokenRound), new PropertyMetadata(false));

        public LLKTokenType TokenType {
            get {
                return (LLKTokenType)GetValue(TokenTypeProperty);
            }
            set {
                SetValue(TokenTypeProperty, value);
            }
        }
        public bool IsSelected {
            get {
                return (bool)GetValue(IsSelectedProperty);
            }
            set {
                SetValue(IsSelectedProperty, value);
            }
        }

        static LLKTokenRound() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LLKTokenRound), new FrameworkPropertyMetadata(typeof(LLKTokenRound)));
        }
        public LLKTokenRound() {

        }
    }
}
