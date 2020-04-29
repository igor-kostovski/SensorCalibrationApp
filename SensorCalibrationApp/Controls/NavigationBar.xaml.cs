using System.Windows;
using System.Windows.Controls;

namespace SensorCalibrationApp.Controls
{
    /// <summary>
    /// Interaction logic for NavigationBar.xaml
    /// </summary>
    public partial class NavigationBar : UserControl
    {
        public static readonly RoutedEvent ShrinkMenuEvent = EventManager.RegisterRoutedEvent("ShrinkMenu", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NavigationBar));
        public static readonly RoutedEvent ExpandMenuEvent = EventManager.RegisterRoutedEvent("ExpandMenu", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NavigationBar));

        public event RoutedEventHandler ShrinkMenu
        {
            add
            {
                this.AddHandler(ShrinkMenuEvent, value);
            }

            remove
            {
                this.RemoveHandler(ShrinkMenuEvent, value);
            }
        }
        public event RoutedEventHandler ExpandMenu
        {
            add
            {
                this.AddHandler(ExpandMenuEvent, value);
            }

            remove
            {
                this.RemoveHandler(ExpandMenuEvent, value);
            }
        }

        private int _count;

        public NavigationBar()
        {
            InitializeComponent();
            _count = 0;
        }

        private void MenuButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_count % 2 == 0)
            {
                RaiseEvent(new RoutedEventArgs(ShrinkMenuEvent));
                this.Resources["TitlesVisibility"] = Visibility.Collapsed;
            }
            else
            {
                RaiseEvent(new RoutedEventArgs(ExpandMenuEvent));
                this.Resources["TitlesVisibility"] = Visibility.Visible;
            }

            _count++;
        }
    }
}
