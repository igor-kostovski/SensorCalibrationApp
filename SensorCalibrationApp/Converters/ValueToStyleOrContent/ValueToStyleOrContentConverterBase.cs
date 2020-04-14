using System;
using System.Linq;
using SensorCalibrationApp.Enums;

namespace SensorCalibrationApp.Converters.ValueToStyleOrContent
{
    public abstract class ValueToStyleOrContentConverterBase
    {
        protected (int,int) Parse(object value, object parameter)
        {
            Screen currentScreen;
            Screen menuItem;

            Enum.TryParse(GetValueString(value), out currentScreen);
            Enum.TryParse(parameter?.ToString(), out menuItem);

            return ((int) currentScreen, (int) menuItem);
        }

        private string GetValueString(object value)
        {
            return value.ToString().Split('.').ElementAt(1);
        }
    }
}
