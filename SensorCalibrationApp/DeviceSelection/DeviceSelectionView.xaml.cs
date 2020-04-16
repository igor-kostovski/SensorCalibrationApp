using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            if (ECU.SelectedIndex == -1)
                ECU.Text = "Select ECU";

            if (device.SelectedIndex == -1)
                device.Text = "Select device";

            if (!frame.IsEnabled)
                frame.Text = "Selected device is currently not supported. Please select another device.";
            else if (frame.SelectedIndex == -1)
                frame.Text = "Select frame";
        }
    }
}
