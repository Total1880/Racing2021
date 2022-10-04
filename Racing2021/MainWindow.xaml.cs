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
        private GameHomeScreenPage _GameHomeScreenPage;
        private NavigationButtonsPage _navigationButtonsPage;
        private TeamPage _teamPage;
        private OtherTeamsPage _otherTeamPage;
        private SearchCyclistPage _searchCyclistPage;
        private CyclistPage _cyclistPage;
        private InitializeRacePage _initializeRacePage;

        public EditorCyclistsPage EditorCyclistsPage => _editorCyclistsPage ??= new EditorCyclistsPage();
        public EditorHomePage EditorHomePage => _editorHomePage ??= new EditorHomePage();
        public EditorTracksPage EditorTracksPage => _editorTracksPage ??= new EditorTracksPage();
        public EditorTeamsPage EditorTeamsPage => _editorTeamsPage ??= new EditorTeamsPage();
        public GameHomeScreenPage GameHomeScreenPage => _GameHomeScreenPage ??= new GameHomeScreenPage();
        public HomePage HomePage => _homePage ??= new HomePage();
        public NavigationButtonsPage NavigationButtonsPage => _navigationButtonsPage ??= new NavigationButtonsPage();
        public TeamPage TeamPage => _teamPage ??= new TeamPage();
        public OtherTeamsPage OtherTeamsPage => _otherTeamPage ??= new OtherTeamsPage();
        public SearchCyclistPage SearchCyclistPage => _searchCyclistPage ??= new SearchCyclistPage();
        public CyclistPage CyclistPage => _cyclistPage ??= new CyclistPage();
        public InitializeRacePage InitializeRacePage => _initializeRacePage ??= new InitializeRacePage();

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.NavigationService.Navigate(HomePage);
            Messenger.Default.Register<OpenEditorCyclistPageMessage>(this, OpenEditorCyclistPage);
            Messenger.Default.Register<OpenEditorHomePageMessage>(this, OpenEditorHomePage);
            Messenger.Default.Register<OpenEditorTracksPageMessage>(this, OpenEditorTracksPage);
            Messenger.Default.Register<OpenEditorTeamsPageMessage>(this, OpenEditorTeamsPage);
            Messenger.Default.Register<OpenGameHomeScreenPageMessage>(this, OpenGameHomeScreenPage);
            Messenger.Default.Register<OpenHomePageMessage>(this, OpenHomePage);
            Messenger.Default.Register<OpenTeamPageMessage>(this, OpenTeamPage);
            Messenger.Default.Register<OpenOtherTeamPageMessage>(this, OpenOtherTeamPage);
            Messenger.Default.Register<OpenSearchCyclistPageMessage>(this, OpenSearchCyclistPage);
            Messenger.Default.Register<OpenCyclistPageMessage>(this, OpenCyclistPage);
            Messenger.Default.Register<OpenInitializeRacePageMessage>(this, OpenInitializeRacePage);
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

        private void OpenGameHomeScreenPage(OpenGameHomeScreenPageMessage obj)
        {
            MainFrame.NavigationService.Navigate(GameHomeScreenPage);
            NavigationFrame.NavigationService.Navigate(NavigationButtonsPage);
        }

        private void OpenHomePage(OpenHomePageMessage obj)
        {
            MainFrame.NavigationService.Navigate(HomePage);
            NavigationFrame.Content = null;
        }

        private void OpenTeamPage(OpenTeamPageMessage obj)
        {
            MainFrame.NavigationService.Navigate(TeamPage);
        }

        private void OpenOtherTeamPage(OpenOtherTeamPageMessage obj)
        {
            MainFrame.NavigationService.Navigate(OtherTeamsPage);
        }

        private void OpenSearchCyclistPage(OpenSearchCyclistPageMessage obj)
        {
            MainFrame.NavigationService.Navigate(SearchCyclistPage);
        }

        private void OpenCyclistPage(OpenCyclistPageMessage obj)
        {
            MainFrame.NavigationService.Navigate(CyclistPage);
        }

        private void OpenInitializeRacePage(OpenInitializeRacePageMessage obj)
        {
            MainFrame.NavigationService.Navigate(InitializeRacePage);
        }
    }
}