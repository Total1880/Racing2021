using Autofac;

namespace Racing2021.RaceEngine
{
    public class RaceEngineModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RaceEngineStart>().AsImplementedInterfaces();
        }
    }
}
