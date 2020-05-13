using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using MaterialDesignThemes.Wpf;

namespace SensorCalibrationApp.Converters
{
    public class ByteArrayToIcon : IValueConverter
    {
        private readonly List<byte[]> _validResponses;

        public ByteArrayToIcon()
        {
            _validResponses = new List<byte[]>
            {
                new byte[]{ 0x20, 0x06, 0xF2, 0x0D, 0x01, 0x02, 0x00, 0x01 },
                new byte[] { 0x20, 0x01, 0xF6, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }
            };
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isValid = false;

            if(value is byte[] response)
                isValid = _validResponses.Any(x => response.SequenceEqual(x));

            if (isValid)
                return Application.Current.FindResource("Success") as PackIcon;

            return Application.Current.FindResource("Error") as PackIcon;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
