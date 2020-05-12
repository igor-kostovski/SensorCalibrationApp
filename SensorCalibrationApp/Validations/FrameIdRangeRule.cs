using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using SensorCalibrationApp.Common.Extensions;

namespace SensorCalibrationApp.Validations
{
    [ContentProperty("ComparisonValue")]
    public class FrameIdRangeRule : ValidationRule
    {
        public ComparisonValue ComparisonValue { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var frameId = (byte) GetValue(value);
            ValidationResult result;

            if (!frameId.IsInRange())
                result = new ValidationResult(false,
                    $"Please enter an frameId in the range: {BitConverter.ToString(new[] { ByteExt.Min })}-{BitConverter.ToString(new[] { ByteExt.Max })}.");
            else
              result = ValidationResult.ValidResult;

            ComparisonValue.RaiseAfterValidation?.Invoke();
            return result;
        }

        private object GetValue(object value)
        {
            if (value is BindingExpression binding)
                return binding.ResolvedSource
                    .GetType()
                    .GetProperty(binding.ResolvedSourcePropertyName)?
                    .GetValue(binding.ResolvedSource, null);

            return value;
        }
    }
}
