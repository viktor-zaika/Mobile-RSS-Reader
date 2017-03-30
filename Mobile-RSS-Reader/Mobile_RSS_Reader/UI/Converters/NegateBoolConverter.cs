using System;
using Xamarin.Forms;

namespace Mobile_RSS_Reader.UI.Converters
{
    /// <summary>
    /// Two way negate converter
    /// </summary>
    public class NegateBooleanConverter : IValueConverter
    {
        /// <inheritdoc /> 
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool) value;
        }

        /// <inheritdoc /> 
        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return !(bool) value;
        }
    }
}