using Autofac;
using Racing2021.Services;
using Racing2021.ViewModels;

namespace Racing2021
{
    public class ViewModelLocator
    {
        private readonly IContainer _container;

        public ViewModelLocator()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<ServicesModule>();

            builder.RegisterType<EditorCyclistsViewModel>().SingleInstance();
            builder.RegisterType<EditorHomePageViewModel>().SingleInstance();
            builder.RegisterType<EditorTracksViewModel>().SingleInstance();
            builder.RegisterType<EditorTeamsViewModel>().SingleInstance();
            builder.RegisterType<GameHomeScreenViewModel>().SingleInstance();
            builder.RegisterType<HomePageViewModel>().SingleInstance();
            builder.RegisterType<NavigationButtonsViewModel>().SingleInstance();
            builder.RegisterType<TeamPageViewModel>().SingleInstance();
            builder.RegisterType<OtherTeamPageViewModel>().SingleInstance();
            builder.RegisterType<SearchCyclistPageViewModel>().SingleInstance();
            builder.RegisterType<CyclistPageViewModel>().SingleInstance();
            builder.RegisterType<InitializeRacePageViewModel>().SingleInstance();

            _container = builder.Build();
        }

        public EditorCyclistsViewModel EditorCyclists => _container.Resolve<EditorCyclistsViewModel>();
        public EditorHomePageViewModel EditorHomePage => _container.Resolve<EditorHomePageViewModel>();
        public EditorTracksViewModel EditorTracks => _container.Resolve<EditorTracksViewModel>();
        public EditorTeamsViewModel EditorTeams => _container.Resolve<EditorTeamsViewModel>();
        public GameHomeScreenViewModel StartRace => _container.Resolve<GameHomeScreenViewModel>();
        public HomePageViewModel HomePage => _container.Resolve<HomePageViewModel>();
        public NavigationButtonsViewModel NavigationButtons => _container.Resolve<NavigationButtonsViewModel>();
        public TeamPageViewModel TeamPage => _container.Resolve<TeamPageViewModel>();
        public OtherTeamPageViewModel OtherTeamPage => _container.Resolve<OtherTeamPageViewModel>();
        public SearchCyclistPageViewModel SearchCyclistPage => _container.Resolve<SearchCyclistPageViewModel>();
        public CyclistPageViewModel CyclistPage => _container.Resolve<CyclistPageViewModel>();
        public InitializeRacePageViewModel InitializeRacePage => _container.Resolve<InitializeRacePageViewModel>();
    }
}
