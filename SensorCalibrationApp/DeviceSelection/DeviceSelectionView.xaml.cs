using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SensorCalibrationApp.DeviceSelection
{
    /// <summary>
    /// Interaction logic for DeviceSelectionView.xaml
    /// </summary>
    public partial class DeviceSelectionView : UserControl
    {
        public DeviceSelectionView()
        {
            InitializeComponent();
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AdjustPlaceholders();
        }

        private void AdjustPlaceholders()
        {
            if (ECU.SelectorSelectedIndex == -1)
                ECU.SelectorText = "Select ECU";

            if (Device.SelectorSelectedIndex == -1)
                Device.SelectorText = "Select device";

            if (!Frame.IsSelectorEnabled)
            {
                Frame.SelectorText = "Selected device is currently not supported. Please select another device.";
                Frame.SelectorForeground = new SolidColorBrush(Colors.Red);
            }
            else if (Frame.SelectorSelectedIndex == -1)
            {
                Frame.SelectorText = "Select frame";
                Frame.SelectorForeground = Application.Current.FindResource("BrushYellow") as SolidColorBrush;
            }
        }
    }
}
