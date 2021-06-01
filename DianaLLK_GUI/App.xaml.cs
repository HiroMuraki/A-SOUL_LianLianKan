using DianaLLK_GUI.ViewModel;
using System;
using System.Windows;

namespace DianaLLK_GUI {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        public static readonly ResourceDictionary ImageDict = new ResourceDictionary() {
            Source = new Uri("Resources/Images.xaml", UriKind.Relative)
        };
        public static readonly ResourceDictionary ColorDict = new ResourceDictionary() {
            Source = new Uri("Resources/PresetColors.xaml", UriKind.Relative)
        };

        private void Application_Startup(object sender, StartupEventArgs e) {
            GameSetter setter = GameSetter.GetInstance();
            setter.RowSize = 6;
            setter.ColumnSize = 10;
            setter.TokenAmount = 15;
            try {
                for (int i = 0; i < e.Args.Length; i++) {
                    string currentArg = e.Args[i].ToUpper();
                    if (currentArg == "-ROW") {
                        setter.RowSize = Convert.ToInt32(e.Args[i + 1]);
                        i += 1;
                    }
                    else if (currentArg == "-COLUMN" || currentArg == "-COL") {
                        setter.ColumnSize = Convert.ToInt32(e.Args[i + 1]);
                        i += 1;
                    }
                    else if (currentArg == "-TYPES") {
                        setter.TokenAmount = Convert.ToInt32(e.Args[i + 1]);
                        i += 1;
                    }
                }
            }
            catch {
                setter.RowSize = 6;
                setter.ColumnSize = 10;
                setter.TokenAmount = 15;
                MessageBox.Show("启动参数解析错误，使用默认值");
            }
            MainWindow window = new MainWindow();
            window.Show();
        }
    }
}
