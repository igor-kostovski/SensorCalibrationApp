using System.Windows.Controls;

namespace SensorCalibrationApp.Controls
{
    /// <summary>
    /// Interaction logic for EmptyResponseFrame.xaml
    /// </summary>
    public partial class EmptyResponseFrame : UserControl
    {
        public EmptyResponseFrame()
        {
            InitializeComponent();
            EmptyResponse.ItemsSource = new string[8];
        }
    }
}
