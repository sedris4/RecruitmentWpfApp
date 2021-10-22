using RecruitmentWpfApp.Interfaces;
using RecruitmentWpfApp.Services;
using RecruitmentWpfApp.ViewModels;

using Serilog;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace RecruitmentWpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            Application.Current.DispatcherUnhandledException += DispatcherOnUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(Path.Combine("Logs", "ApplicationLog.log"), rollingInterval: RollingInterval.Day)
                .CreateLogger();

            IServiceLocator locator = ServiceLocator.Create();

            MainWindow = locator.Resolve<MainWindow>();
            MainWindow.DataContext = locator.Resolve<MainViewModel>();
            MainWindow.Show();
        }

        private void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Log.Logger.Error("TaskScheduler unobserved exception", e.Exception);
        }

        private void DispatcherOnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Log.Logger.Error("Dispatcher unhandled exception", e.Exception);
        }

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Logger.Error("CurrentDomain unhandled exception", e.ExceptionObject as Exception);
        }
    }
}
