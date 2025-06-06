using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace final.Infrastructure.Converters
{
    class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
            {
                bool invert = (parameter as string)?.Equals("Invert", StringComparison.OrdinalIgnoreCase) ?? false;

                if (invert)
                    return b ? Visibility.Collapsed : Visibility.Visible;
                else
                    return b ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility v)
            {
                bool invert = (parameter as string)?.Equals("Invert", StringComparison.OrdinalIgnoreCase) ?? false;

                if (invert)
                    return v != Visibility.Visible;
                else
                    return v == Visibility.Visible;
            }

            return false;
        }
    }
}
