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
            LLKTokenType tokenType = (LLKTokenType)value;
            switch (tokenType) {
                case LLKTokenType.None:
                    return null;
                case LLKTokenType.AS:
                    return App.ColorDict["ASTheme"] as SolidColorBrush;
                case LLKTokenType.A1:
                case LLKTokenType.A2:
                case LLKTokenType.A3:
                case LLKTokenType.A4:
                case LLKTokenType.A5:
                    return App.ColorDict["AvaTheme"] as SolidColorBrush;
                case LLKTokenType.B1:
                case LLKTokenType.B2:
                case LLKTokenType.B3:
                case LLKTokenType.B4:
                case LLKTokenType.B5:
                    return App.ColorDict["BellaTheme"] as SolidColorBrush;
                case LLKTokenType.C1:
                case LLKTokenType.C2:
                case LLKTokenType.C3:
                case LLKTokenType.C4:
                case LLKTokenType.C5:
                    return App.ColorDict["CarolTheme"] as SolidColorBrush;
                case LLKTokenType.D1:
                case LLKTokenType.D2:
                case LLKTokenType.D3:
                case LLKTokenType.D4:
                case LLKTokenType.D5:
                    return App.ColorDict["DianaTheme"] as SolidColorBrush;
                case LLKTokenType.E1:
                case LLKTokenType.E2:
                case LLKTokenType.E3:
                case LLKTokenType.E4:
                case LLKTokenType.E5:
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
