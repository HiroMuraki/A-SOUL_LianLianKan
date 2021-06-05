using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DianaLLK_GUI.View {
    /// <summary>
    /// TipBar.xaml 的交互逻辑
    /// </summary>
    public partial class TipBar : UserControl {
        public object Tip {
            get {
                return (object)GetValue(TipProperty);
            }
            set {
                SetValue(TipProperty, value);
            }
        }
        public static readonly DependencyProperty TipProperty =
            DependencyProperty.Register(nameof(Tip), typeof(object), typeof(TipBar), new PropertyMetadata(null));

        public TipBar() {
            InitializeComponent();
        }

        public async void DisplayTip(object tip, TimeSpan displayTime) {
            Tip = tip;
            DoubleAnimation animation = new DoubleAnimation() {
                To = 40,
                AccelerationRatio = 0.2,
                DecelerationRatio = 0.8,
                Duration = TimeSpan.FromMilliseconds(150)
            };
            BeginAnimation(HeightProperty, animation);
            await Task.Delay(displayTime);
            DoubleAnimation animation2 = new DoubleAnimation() {
                To = 0,
                AccelerationRatio = 0.2,
                DecelerationRatio = 0.8,
                Duration = TimeSpan.FromMilliseconds(150)
            };
            BeginAnimation(HeightProperty, animation2);
            Tip = null;
        }
    }
}
