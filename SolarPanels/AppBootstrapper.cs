using Caliburn.Micro;
using System;
using Autofac.Core;
using Autofac;
using SolarPanels.ViewModels;
using System.Windows;

namespace SolarPanels
{
    public class AppBootstrapper : BootstrapperBase
    {
        private IContainer _container;

        public AppBootstrapper() => Initialize();

        protected override void Configure()
        {
            var builder = new Autofac.ContainerBuilder();

            builder.RegisterType<MainViewModel>().SingleInstance();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<WindowManager>().As<IWindowManager>().SingleInstance();

            _container = builder.Build();


        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainViewModel>();
        }

        protected override object GetInstance(Type serviceType, string key) => _container.Resolve(serviceType);
    }

}
