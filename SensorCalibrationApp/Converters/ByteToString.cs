using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace SensorCalibrationApp.Converters
{
    public class ByteToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            byte byteVal;
            if (value is byte b)
                byteVal = b;
            else
                return "";

            return BitConverter.ToString(new[] {byteVal});
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var strVal = value as string;
            var intVal = System.Convert.ToInt64(strVal, 16);

            var test =  BitConverter.GetBytes(intVal).First();
            return test;
        }
    }
}
