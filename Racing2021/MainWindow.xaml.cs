using GalaSoft.MvvmLight.Messaging;
using Racing2021.Messages.WindowOpener;
using Racing2021.Pages;
using System;
using System.Windows;

namespace Racing2021
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HomePage _homePage;
        private StartRacePage _startRacePage;

        public StartRacePage StartRacePage => _startRacePage ??= new StartRacePage();
        public HomePage HomePage => _homePage ??= new HomePage();

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.NavigationService.Navigate(HomePage);
            Messenger.Default.Register<OpenStartRacePageMessage>(this, OpenStartRacePage);
        }

        private void OpenStartRacePage(OpenStartRacePageMessage obj)
        {
            MainFrame.NavigationService.Navigate(StartRacePage);
        }
    }
}
