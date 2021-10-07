﻿using Autofac;

namespace Racing2021.Repositories
{
    public class RepositoriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CyclistRepository>().AsImplementedInterfaces();
            builder.RegisterType<TrackRepository>().AsImplementedInterfaces();
            builder.RegisterType<TeamRepository>().AsImplementedInterfaces();
        }
    }
}
