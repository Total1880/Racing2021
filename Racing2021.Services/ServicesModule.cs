using Autofac;
using Racing2021.RaceEngine;
using Racing2021.Repositories;

namespace Racing2021.Services
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<RaceEngineModule>();
            builder.RegisterModule<RepositoriesModule>();

            builder.RegisterType<RaceService>().AsImplementedInterfaces();
            builder.RegisterType<CyclistService>().AsImplementedInterfaces();
            builder.RegisterType<TrackService>().AsImplementedInterfaces();
            builder.RegisterType<SeasonService>().AsImplementedInterfaces();
            builder.RegisterType<TeamService>().AsImplementedInterfaces();
            builder.RegisterType<DivisionService>().AsImplementedInterfaces();
            builder.RegisterType<DataService>().AsImplementedInterfaces();
            builder.RegisterType<AIManagerService>().AsImplementedInterfaces();
            builder.RegisterType<ManagerService>().AsImplementedInterfaces();
        }
    }
}
