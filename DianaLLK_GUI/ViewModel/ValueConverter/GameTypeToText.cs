using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using LianLianKan;

namespace DianaLLK_GUI.ViewModel.ValueConverter {
    [ValueConversion(typeof(GameType), typeof(string))]
    public class GameTypeToText : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            GameType gameType = (GameType)value;
            switch (gameType) {
                case GameType.New:
                    return "新游戏";
                case GameType.Restored:
                    return "读档游戏";
                default:
                    return " 未知";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
