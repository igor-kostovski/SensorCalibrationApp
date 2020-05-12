using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace SensorCalibrationApp.Validations
{
    public class LengthRangeRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var length = (byte)GetValue(value);

            if (length > 8 || length < 1)
                return new ValidationResult(false,
                    $"Please enter length in the range: { 1 } - { 8 }.");
            return ValidationResult.ValidResult;
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
