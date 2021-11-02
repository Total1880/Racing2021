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

            builder.RegisterType<EditorHomePageViewModel>().SingleInstance();
            builder.RegisterType<StartRaceViewModel>().SingleInstance();
            builder.RegisterType<HomePageViewModel>().SingleInstance();

            _container = builder.Build();
        }

        public EditorHomePageViewModel EditorHomePage => _container.Resolve<EditorHomePageViewModel>();
        public StartRaceViewModel StartRace => _container.Resolve<StartRaceViewModel>();
        public HomePageViewModel HomePage => _container.Resolve<HomePageViewModel>();
    }
}
