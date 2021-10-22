using Autofac;
using Autofac.Core;

using RecruitmentWpfApp.Database;
using RecruitmentWpfApp.Interfaces;
using RecruitmentWpfApp.ViewModels;
using RecruitmentWpfApp.Views;

using System;
using System.Threading.Tasks;

namespace RecruitmentWpfApp.Services
{
    /// <summary>
    /// IoC container
    /// </summary>
    public class ServiceLocator : IServiceLocator
    {
        public static IServiceLocator Instance { get; private set; } 

        /// <summary>
        /// Container instance
        /// </summary>
        private readonly IContainer _container;

        /// <summary>
        /// Initializes new instance of <see cref="ServiceLocator"/>
        /// </summary>
        private ServiceLocator()
        {
            _container = BuildupContainer(new ContainerBuilder()).Build();
        }

        /// <summary>
        /// Create new <see cref="IServiceLocator"/>
        /// </summary>
        /// <returns></returns>
        public static IServiceLocator Create()
        {
            return Instance = new ServiceLocator();
        }

        private ContainerBuilder BuildupContainer(ContainerBuilder builder)
        {
            builder.RegisterInstance(this).As<IServiceLocator>();

            builder.RegisterType<ApplicationLogger>().As<IApplicationLogger>();
            builder.RegisterType<FileSelectionService>().As<IFilepathSelector>();
            builder.RegisterType<PersonDataLoadersBank>().As<IPersonDataLoadersBank>();
            builder.RegisterType<MessageBoxService>().As<IMessageBoxService>();

            builder.RegisterType<DatabaseContext>().As<IDatabaseContext>();
            builder.RegisterType<PeopleRepository>().As<IPeopleRepository>().SingleInstance();

            builder.RegisterType<DataEditorViewModel>();
            builder.RegisterType<DataEditionWindow>();
            builder.RegisterType<MainViewModel>();
            builder.RegisterType<MainWindow>();

            return builder;
        }

        /// <summary>
        /// Resolves for a generic type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
        /// <summary>
        /// Resolves for a generic type with parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public T Resolve<T>(params Parameter[] parameters)
        {
            return _container.Resolve<T>(parameters);
        }
    }
}
