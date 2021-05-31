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
                default:
                    return new SolidColorBrush(Colors.Gold);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
