using Autofac;
using Racing2021.Services;
using Racing2021.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racing2021
{
    public class ViewModelLocator
    {
        private readonly IContainer _container;

        public ViewModelLocator()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<ServicesModule>();

            builder.RegisterType<StartRaceViewModel>().SingleInstance();

            _container = builder.Build();
        }

        public StartRaceViewModel StartRace => _container.Resolve<StartRaceViewModel>();
    }
}
