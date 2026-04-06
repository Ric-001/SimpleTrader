using System.Globalization;
using System.Windows.Data;

namespace SimpleTrader.WPF.Converters
{
    public class EmptyStringToZeroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString() ?? "0";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = value as string;

            if (string.IsNullOrWhiteSpace(text))
                return 0; // Devuelve 0 en vez de error

            if (targetType == typeof(int) && int.TryParse(text, out int intResult))
                return intResult;

            if (targetType == typeof(decimal) && decimal.TryParse(text, out decimal decResult))
                return decResult;

            return 0;
        }
    }
}
