using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;

namespace SensorCalibrationApp.Controls
{
    public delegate void SelectionChanged(object sender, SelectionChangedEventArgs e);
    /// <summary>
    /// Interaction logic for Selector.xaml
    /// </summary>
    public partial class Selector : UserControl
    {
        public Selector()
        {
            InitializeComponent();

            Card.DataContext = this;
        }

        public SolidColorBrush ThemeBrush
        {
            get { return (SolidColorBrush)GetValue(ThemeBrushProperty); }
            set
            {
                SetValue(ThemeBrushProperty, value);
            }
        }

        public static readonly DependencyProperty ThemeBrushProperty =
            DependencyProperty.Register(
                nameof(ThemeBrush),
                typeof(SolidColorBrush),
                typeof(Selector),
                new PropertyMetadata(null));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                nameof(Title), 
                typeof(string), 
                typeof(Selector), 
                new PropertyMetadata(null));

        public object ItemsSource
        {
            get { return GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(nameof(ItemsSource), typeof(object), typeof(Selector), new PropertyMetadata(null));

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(Selector), new PropertyMetadata(null));

        public PackIconKind IconKind
        {
            get { return (PackIconKind)GetValue(IconKindProperty); }
            set { SetValue(IconKindProperty, value); }
        }

        public static readonly DependencyProperty IconKindProperty =
            DependencyProperty.Register(nameof(IconKind), typeof(PackIconKind), typeof(Selector), new PropertyMetadata(null));

        public string SelectorText
        {
            get { return (string)GetValue(SelectorTextProperty); }
            set { SetValue(SelectorTextProperty, value); }
        }

        public static readonly DependencyProperty SelectorTextProperty =
            DependencyProperty.Register(nameof(SelectorText), typeof(string), typeof(Selector), new PropertyMetadata(null));

        public bool IsSelectorEnabled
        {
            get { return (bool)GetValue(IsSelectorEnabledProperty); }
            set { SetValue(IsSelectorEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsSelectorEnabledProperty =
            DependencyProperty.Register(nameof(IsSelectorEnabled), typeof(bool), typeof(Selector), new PropertyMetadata(null));


        public SelectionChanged SelectorSelectionChanged
        {
            get { return (SelectionChanged)GetValue(SelectorSelectionChangedProperty); }
            set { SetValue(SelectorSelectionChangedProperty, value); }
        }

        public static readonly DependencyProperty SelectorSelectionChangedProperty =
            DependencyProperty.Register(nameof(SelectorSelectionChanged), typeof(SelectionChanged), typeof(Selector), new PropertyMetadata(null));

        public int SelectorSelectedIndex
        {
            get { return (int)GetValue(SelectorSelectedIndexProperty); }
            set { SetValue(SelectorSelectedIndexProperty, value); }
        }

        public static readonly DependencyProperty SelectorSelectedIndexProperty =
            DependencyProperty.Register(nameof(SelectorSelectedIndex), typeof(int), typeof(Selector), new PropertyMetadata(0));

        public SolidColorBrush SelectorForeground
        {
            get { return (SolidColorBrush)GetValue(SelectorForegroundProperty); }
            set { SetValue(SelectorForegroundProperty, value); }
        }

        public static readonly DependencyProperty SelectorForegroundProperty =
            DependencyProperty.Register(nameof(SelectorForeground), typeof(SolidColorBrush), typeof(Selector), new PropertyMetadata(null));

        private void ComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectorSelectionChanged?.Invoke(this, null);
        }
    }
}
