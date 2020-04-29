using System.Windows;

namespace SensorCalibrationApp.Validations
{
    public delegate void NotifyForValidation();

    public class ComparisonValue : DependencyObject
    {
        public NotifyForValidation RaiseAfterValidation
        {
            get { return (NotifyForValidation)GetValue(RaiseAfterValidationProperty); }
            set
            {
                SetValue(RaiseAfterValidationProperty, value);
            }
        }

        public static readonly DependencyProperty RaiseAfterValidationProperty =
            DependencyProperty.Register(nameof(RaiseAfterValidation),
                typeof(NotifyForValidation), typeof(ComparisonValue), new FrameworkPropertyMetadata(default(NotifyForValidation)));
    }
}
