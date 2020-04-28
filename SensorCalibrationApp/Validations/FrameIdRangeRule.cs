using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using SensorCalibrationApp.Diagnostics;

namespace SensorCalibrationApp.Validations
{
    public class FrameIdRangeRule : ValidationRule
    {
        public byte Max { get; set; }
        public byte Min { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            byte frameId;

            try
            {
                frameId = (byte) GetValue(value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Illegal characters or {e.Message}");
            }

            if ((frameId < Min) || (frameId > Max))
            {
                return new ValidationResult(false,
                    $"Please enter an frameId in the range: {BitConverter.ToString(new[] { Min })}-{BitConverter.ToString(new[] { Max })}.");
            }
            return ValidationResult.ValidResult;
        }

        private object GetValue(object value)
        {
            if (value is BindingExpression)
            {
                BindingExpression binding = (BindingExpression)value;
                
                object dataItem = binding.DataItem;

                if (dataItem is Signal)
                    return (dataItem as Signal).Value;
            }

            return value;
        }
    }
}
