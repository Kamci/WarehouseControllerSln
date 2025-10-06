using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace WarehouseController.Converters
{
    public class DecimalToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal decimalValue)
                return decimalValue.ToString("0.##", CultureInfo.CurrentCulture); 
            return "0";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str && decimal.TryParse(str, NumberStyles.Any, CultureInfo.CurrentCulture, out decimal result)) 
                return result;
            return 0m;
        }
    }
}