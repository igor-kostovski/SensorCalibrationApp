using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace SensorCalibrationApp.Converters
{
    public class ByteToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var byteVal = (byte) value;

            return BitConverter.ToString(new[] {byteVal});
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var strVal = value as string;
            long intVal;
            try
            {
                intVal = System.Convert.ToInt64(strVal, 16);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return 0x00;
            }

            return BitConverter.GetBytes(intVal).First();
        }
    }
}
