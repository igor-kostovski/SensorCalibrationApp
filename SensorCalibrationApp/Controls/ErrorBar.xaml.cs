using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SensorCalibrationApp.Common;

namespace SensorCalibrationApp.Controls
{
    /// <summary>
    /// Interaction logic for ErrorBar.xaml
    /// </summary>
    public partial class ErrorBar : UserControl
    {
        public static readonly DependencyProperty ErrorProperty =
            DependencyProperty.Register("Error", typeof(ErrorMessage), typeof(ErrorBar), 
                new PropertyMetadata(default(ErrorMessage), PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject dependencyObject, 
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var ctrl = dependencyObject as ErrorBar;
            if (ctrl != null)
            {
                if (dependencyPropertyChangedEventArgs.NewValue != null)
                {
                    ctrl.BeginFadingAnimation();
                }
            }
        }

        public ErrorMessage Error
        {
            get { return (ErrorMessage)GetValue(ErrorProperty); }
            set
            {
                SetValue(ErrorProperty, value);
            }
        }

        public ErrorBar()
        {
            InitializeComponent();
            Bar.DataContext = this;
        }

        private void BeginFadingAnimation()
        {
            Storyboard sbLoaded = Bar.Resources["LoadedStoryboard"] as Storyboard;
            sbLoaded.Completed += (sender, args) =>
            {
                Storyboard sbUnLoaded = Bar.Resources["UnloadedStoryboard"] as Storyboard;
                sbUnLoaded.Begin();
            };

            sbLoaded.Begin();
        }
    }
}
