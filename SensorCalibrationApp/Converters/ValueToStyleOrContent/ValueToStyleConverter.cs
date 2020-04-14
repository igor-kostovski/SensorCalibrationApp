﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SensorCalibrationApp.Converters.ValueToStyleOrContent
{
    public class ValueToStyleConverter : ValueToStyleOrContentConverterBase, IValueConverter
    {
        public string IconOrTitle { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var (currScreenIndex, menuItemIndex) = Parse(value, parameter);

            if (currScreenIndex > menuItemIndex)
                return Application.Current.FindResource($"Passed{IconOrTitle}Style") as Style;

            if (currScreenIndex < menuItemIndex)
                return Application.Current.FindResource($"Unvisited{IconOrTitle}Style") as Style;

            return Application.Current.FindResource($"Current{IconOrTitle}Style") as Style;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
