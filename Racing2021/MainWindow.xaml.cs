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
        private EditorCyclistsPage _editorCyclistsPage;
        private EditorHomePage _editorHomePage;
        private EditorTracksPage _editorTracksPage;
        private EditorTeamsPage _editorTeamsPage;
        private HomePage _homePage;
        private StartRacePage _startRacePage;
        private NavigationButtonsPage _navigationButtonsPage;

        public EditorCyclistsPage EditorCyclistsPage => _editorCyclistsPage ??= new EditorCyclistsPage();
        public EditorHomePage EditorHomePage => _editorHomePage ??= new EditorHomePage();
        public EditorTracksPage EditorTracksPage => _editorTracksPage ??= new EditorTracksPage();
        public EditorTeamsPage EditorTeamsPage => _editorTeamsPage ??= new EditorTeamsPage();
        public StartRacePage StartRacePage => _startRacePage ??= new StartRacePage();
        public HomePage HomePage => _homePage ??= new HomePage();
        public NavigationButtonsPage NavigationButtonsPage => _navigationButtonsPage ??= new NavigationButtonsPage();

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.NavigationService.Navigate(HomePage);
            Messenger.Default.Register<OpenEditorCyclistPageMessage>(this, OpenEditorCyclistPage);
            Messenger.Default.Register<OpenEditorHomePageMessage>(this, OpenEditorHomePage);
            Messenger.Default.Register<OpenEditorTracksPageMessage>(this, OpenEditorTracksPage);
            Messenger.Default.Register<OpenEditorTeamsPageMessage>(this, OpenEditorTeamsPage);
            Messenger.Default.Register<OpenStartRacePageMessage>(this, OpenStartRacePage);
            Messenger.Default.Register<OpenHomePageMessage>(this, OpenHomePage);
        }

        private void OpenEditorCyclistPage(OpenEditorCyclistPageMessage obj)
        {
            MainFrame.NavigationService.Navigate(EditorCyclistsPage);
        }

        private void OpenEditorHomePage(OpenEditorHomePageMessage obj)
        {
            MainFrame.NavigationService.Navigate(EditorHomePage);
            NavigationFrame.NavigationService.Navigate(NavigationButtonsPage);
        }

        private void OpenEditorTracksPage(OpenEditorTracksPageMessage obj)
        {
            MainFrame.NavigationService.Navigate(EditorTracksPage);
        }

        private void OpenEditorTeamsPage(OpenEditorTeamsPageMessage obj)
        {
            MainFrame.NavigationService.Navigate(EditorTeamsPage);
        }

        private void OpenStartRacePage(OpenStartRacePageMessage obj)
        {
            MainFrame.NavigationService.Navigate(StartRacePage);
            NavigationFrame.NavigationService.Navigate(NavigationButtonsPage);

        }

        private void OpenHomePage(OpenHomePageMessage obj)
        {
            MainFrame.NavigationService.Navigate(HomePage);
            NavigationFrame.Content = null;
        }
    }
}