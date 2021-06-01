using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace DianaLLK_GUI.ViewModel.ValueConverter {
    public class GameThemeToColor : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            GameTheme theme = (GameTheme)value;
            switch (theme) {
                case GameTheme.None:
                    return Colors.White;
                case GameTheme.Ava:
                    return App.ColorDict["AvaTheme"] as SolidColorBrush;
                case GameTheme.Bella:
                    return App.ColorDict["BellaTheme"] as SolidColorBrush;
                case GameTheme.Carol:
                    return App.ColorDict["CarolTheme"] as SolidColorBrush;
                case GameTheme.Diana:
                    return App.ColorDict["DianaTheme"] as SolidColorBrush;
                case GameTheme.Eileen:
                    return App.ColorDict["EileenTheme"] as SolidColorBrush;
                default:
                    return Colors.White;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
