using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace Organizer.Infrastructure.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool v && v == true) return Brushes.LimeGreen;
            else return Brushes.Transparent;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return null;
        }
    }

}
