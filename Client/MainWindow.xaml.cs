using Client.Models;
using System.Windows;
using System.Windows.Controls;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new ViewWeatherModel();

            InitializeComponent();
        }

        internal void SetStartElemntHour(object sender, SelectionChangedEventArgs args)
        {

            if (sender is ComboBox)
            {
                hour.SelectedIndex = 0;

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ViewWeatherModel();
            hour.SelectedIndex = -1;
        }
    }
}
