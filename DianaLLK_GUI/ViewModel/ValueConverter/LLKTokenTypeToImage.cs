//using MergeDiana.GameLib;
using LianLianKan;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace DianaLLK_GUI.ViewModel.ValueConverter {
    [ValueConversion(typeof(LLKTokenType), typeof(Brush))]
    public class LLKTokenTypeToImage : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            try {
                LLKTokenType tokenType = (LLKTokenType)value;
                return App.ImageDict[LLKHelper.TokenResources[tokenType]] as ImageBrush;
            }
            catch (Exception) {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
