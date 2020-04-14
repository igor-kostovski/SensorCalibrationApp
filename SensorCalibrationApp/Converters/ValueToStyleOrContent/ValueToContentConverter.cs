using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SensorCalibrationApp.Converters.ValueToStyleOrContent
{
    public class ValueToContentConverter : ValueToStyleOrContentConverterBase, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var (currScreenIndex, menuItemIndex) = Parse(value, parameter);

            if (currScreenIndex > menuItemIndex)
                return Application.Current.FindResource("FullStepBar") as Border;

            if (currScreenIndex < menuItemIndex)
                return Application.Current.FindResource("EmptyStepBar") as Border;

            return Application.Current.FindResource("SemiFullStepBar") as Border;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
