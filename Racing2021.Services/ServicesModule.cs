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
        }
    }
}
