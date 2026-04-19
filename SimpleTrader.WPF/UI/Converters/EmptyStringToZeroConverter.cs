using System.Globalization;
using System.Windows.Data;

namespace SimpleTrader.WPF.UI.Converters
{
    public class EmptyStringToZeroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int i) return i.ToString("N0", culture);
            if (value is decimal d) return d.ToString("N0", culture);
            return "0";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = value as string;

            if (string.IsNullOrWhiteSpace(text))
                return 0; // Devuelve 0 en vez de error

            if (targetType == typeof(int) && int.TryParse(text, NumberStyles.AllowThousands | NumberStyles.Integer, culture, out int intResult))
                return intResult;

            if (targetType == typeof(decimal) && decimal.TryParse(text, NumberStyles.AllowThousands | NumberStyles.Number, culture, out decimal decResult))
                return decResult;

            return 0;
        }
    }
}
