using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace SensorCalibrationApp.Validations
{
    public class RequiredRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var field = (string)GetValue(value);

            if (string.IsNullOrWhiteSpace(field))
                return new ValidationResult(false,
                    "This field is required.");
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
