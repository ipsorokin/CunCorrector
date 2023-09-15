using System;
using System.Globalization;
using System.Windows.Data;

namespace CunCorrector.Converters
{
    internal class HardwareDisplayNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (AppVariable.HardwareConstants.ContainsKey((string)value))
                return $"[{AppVariable.HardwareConstants[(string)value]}]";
            return "[UNDEFINED]";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
