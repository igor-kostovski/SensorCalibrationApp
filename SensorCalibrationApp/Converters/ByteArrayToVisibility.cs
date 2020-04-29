using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SensorCalibrationApp.Converters
{
    public class ByteArrayToVisibility : IValueConverter
    {
        public bool Negate { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var byteArr = value as byte[];

            if (byteArr == null && !Negate)
                return Visibility.Collapsed;

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
