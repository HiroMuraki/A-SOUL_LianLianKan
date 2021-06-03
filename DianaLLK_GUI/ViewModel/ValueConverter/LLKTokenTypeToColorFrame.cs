// using MergeDiana.GameLib;
using LianLianKan;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace DianaLLK_GUI.ViewModel.ValueConverter {
    [ValueConversion(typeof(LLKTokenType), typeof(Brush))]
    public class LLKTokenTypeToColorFrame : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            TokenCategory tokenCategory = LLKHelper.GetTokenCategoryFromTokenType((LLKTokenType)value);
            switch (tokenCategory) {
                case TokenCategory.None:
                    return null;
                case TokenCategory.AS:
                    return App.ColorDict["ASTheme"] as SolidColorBrush;
                case TokenCategory.Ava:
                    return App.ColorDict["AvaTheme"] as SolidColorBrush;
                case TokenCategory.Bella:
                    return App.ColorDict["BellaTheme"] as SolidColorBrush;
                case TokenCategory.Carol:
                    return App.ColorDict["CarolTheme"] as SolidColorBrush;
                case TokenCategory.Diana:
                    return App.ColorDict["DianaTheme"] as SolidColorBrush;
                case TokenCategory.Eileen:
                    return App.ColorDict["EileenTheme"] as SolidColorBrush;
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
