using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace SensorCalibrationApp.Screens.FrameConfiguration
{
    /// <summary>
    /// Interaction logic for FrameConfigurationView.xaml
    /// </summary>
    public partial class FrameConfigurationView : UserControl
    {
        public FrameConfigurationView()
        {
            InitializeComponent();
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            Storyboard sb = ((Grid) sender).Resources["LoadedStoryboard"] as Storyboard;
            sb.Begin();
        }
    }
}
