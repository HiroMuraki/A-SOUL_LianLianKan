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
    /// FileDragControl.xaml 的交互逻辑
    /// </summary>
    public partial class FileDragControl : UserControl {
        public event DragEventHandler FileDraged;
        public FileDragControl() {
            InitializeComponent();
        }

        private void Grid_Drop(object sender, DragEventArgs e) {
            FileDraged?.Invoke(this, e);
        }
    }
}
