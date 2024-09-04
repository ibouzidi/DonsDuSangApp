using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace DonsDuSangApp.Converters
{
    public class ShortToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is short shortValue)
            {
                return (int)shortValue;
            }

            return value; // Return the original value if it's not a short
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                return (short)intValue;
            }

            return value; // Return the original value if it's not an int
        }
    }
}
