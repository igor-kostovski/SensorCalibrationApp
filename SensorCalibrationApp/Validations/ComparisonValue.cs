using System.Windows;

namespace SensorCalibrationApp.Validations
{
    public delegate void ValidationNotifier();

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
