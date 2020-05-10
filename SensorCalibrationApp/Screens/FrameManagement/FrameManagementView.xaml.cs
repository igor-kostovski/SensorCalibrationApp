using System.Windows;
using System.Windows.Controls;

namespace SensorCalibrationApp.Screens.FrameManagement
{
    /// <summary>
    /// Interaction logic for FrameManagementView.xaml
    /// </summary>
    public partial class FrameManagementView : UserControl
    {
        public FrameManagementView()
        {
            InitializeComponent();
            ItemsControl.ItemsSource = new string[8];
        }

        private void ClearButton_OnClick(object sender, RoutedEventArgs e)
        {
            FrameComboBox.Text = "";
        }
    }
}
