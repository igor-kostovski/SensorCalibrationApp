using System;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using SensorCalibrationApp.Common;
using SensorCalibrationApp.Common.Extensions;

namespace SensorCalibrationApp.Validations
{
    [ContentProperty("ComparisonValue")]
    public class FrameIdRangeRule : ValidationRule
    {
        public ComparisonValue ComparisonValue { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var signal = GetValue(value);
            ComparisonValue.RaiseAfterValidation?.Invoke();

            if (!signal.CheckRange)
                return ValidationResult.ValidResult;

            if (signal.Value.IsInRange(signal.MinValue, signal.MaxValue))
                return ValidationResult.ValidResult;

            var errorMessage = $"Please enter value in the range: 0x{BitConverter.ToString(new[] { signal.MinValue })} - 0x{BitConverter.ToString(new[] { signal.MaxValue })}\n";

            if (signal.AllowedBytes != null)
            {
                if(signal.AllowedBytes.Contains(signal.Value))
                    return ValidationResult.ValidResult;

                errorMessage += $"or one of the following: {string.Join(",", signal.AllowedBytes.Select(x => $"0x{BitConverter.ToString(new[] { x })}"))}";
            }

            return new ValidationResult(false, errorMessage);
        }

        private Signal GetValue(object value)
        {
            if (value is BindingExpression binding)
                return binding.ResolvedSource as Signal;

            return null;
        }
    }
}
