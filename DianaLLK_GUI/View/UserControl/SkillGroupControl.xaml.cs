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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DianaLLK_GUI.View {
    /// <summary>
    /// SkillGroupControl.xaml 的交互逻辑
    /// </summary>
    public partial class SkillGroupControl : UserControl {
        public event EventHandler<SClickEventArgs> SkillActived;

        public SkillGroupControl() {
            InitializeComponent();
        }

        private void ActiveSkill_Click(object sender, SClickEventArgs e) {
            SkillActived?.Invoke(this, new SClickEventArgs(e.SKill));
        }
    }
}
