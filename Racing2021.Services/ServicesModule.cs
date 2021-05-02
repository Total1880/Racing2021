using Autofac;
using Racing2021.RaceEngine;

namespace Racing2021.Services
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<RaceEngineModule>();

            builder.RegisterType<RaceService>().AsImplementedInterfaces();
        }
    }
}
