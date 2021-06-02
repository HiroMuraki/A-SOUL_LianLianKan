using LianLianKan;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace DianaLLK_GUI.View {
    /// <summary>
    /// SkillDisplayer.xaml 的交互逻辑
    /// </summary>
    public partial class SkillDisplayer : UserControl {
        public SkillDisplayer() {
            InitializeComponent();
        }

        public async void DisplaySkill(LLKSkill skill, double displayTime, double displayWidth) {
            SkillIcon.Content = LLKHelper.GetSkillDescription(skill);

            SkillIcon.Opacity = 1;
            HorizontalAlignment = HorizontalAlignment.Left;
            DoubleAnimation animation = new DoubleAnimation() {
                To = displayWidth,
                AccelerationRatio = 0.2,
                DecelerationRatio = 0.8,
                Duration = TimeSpan.FromMilliseconds(150)
            };
            BeginAnimation(WidthProperty, animation);

            await Task.Delay(TimeSpan.FromMilliseconds(displayTime));

            HorizontalAlignment = HorizontalAlignment.Right;
            DoubleAnimation animation2 = new DoubleAnimation() {
                To = 0,
                AccelerationRatio = 0.2,
                DecelerationRatio = 0.8,
                Duration = TimeSpan.FromMilliseconds(150)
            };
            BeginAnimation(WidthProperty, animation2);
            SkillIcon.Opacity = 0;
        }
    }
}
