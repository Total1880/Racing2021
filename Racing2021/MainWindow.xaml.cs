using Racing2021.Pages;
using System.Windows;

namespace Racing2021
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StartRacePage _startRacePage;

        public StartRacePage StartRacePage => _startRacePage ??= new StartRacePage();

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.NavigationService.Navigate(StartRacePage);
        }
    }
}
