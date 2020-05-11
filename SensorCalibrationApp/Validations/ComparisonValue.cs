using System.Windows;
using SensorCalibrationApp.Common;

namespace SensorCalibrationApp.Validations
{
    public class ComparisonValue : DependencyObject
    {
        public ValidationNotifier RaiseAfterValidation
        {
            get { return (ValidationNotifier)GetValue(RaiseAfterValidationProperty); }
            set
            {
                SetValue(RaiseAfterValidationProperty, value);
            }
        }

        public static readonly DependencyProperty RaiseAfterValidationProperty =
            DependencyProperty.Register(nameof(RaiseAfterValidation),
                typeof(ValidationNotifier), typeof(ComparisonValue), new FrameworkPropertyMetadata(default(ValidationNotifier)));
    }
}
