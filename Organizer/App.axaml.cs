using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using DryIoc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Organizer.Infrastructure;
using Organizer.Infrastructure.Factories;
using Organizer.Infrastructure.Interfaces;
using Organizer.Infrastructure.Services;
using Organizer.Models;
using Organizer.Models.Configs;
using Organizer.Services;
using Organizer.ViewModels;
using Organizer.ViewModels.Controlls;
using Organizer.ViewModels.Windows;
using Organizer.Views;
using ReactiveUI;
using Splat;
using Splat.DryIoc;
using System;
using System.IO;
using System.Linq;

namespace Organizer
{
    public partial class App : Application
    {

        protected IContainer? Container;

        public override void Initialize()
        {
            InitializeDI();
            InitializeModules();
            AvaloniaXamlLoader.Load(this);
        }

        private void InitializeDI()
        {
            var container = new Container();

            container.RegisterDelegate(context =>
            {
                var pathService = context.Resolve<PathsService>();
                string connectionStr = new SqliteConnectionStringBuilder { DataSource = Path.Combine(pathService.DbDirectory, "data.db") }.ToString();

                return new DbContextOptionsBuilder().UseSqlite(connectionStr).Options;
            });
            container.Register<DataContext>(setup: Setup.With(allowDisposableTransient: true));

            container.Register<MainWindow>(Reuse.Singleton);
            container.Register<ViewModelFactory>(Reuse.Singleton);

            //Register Services
            container.RegisterMany<DialogService>(Reuse.Singleton);
            container.RegisterMany<DatabaseService>(Reuse.Singleton);
            container.RegisterMany<NavigationService>(Reuse.Singleton);
            container.Register<PathsService>(Reuse.Singleton);
            container.Register<ItemsService>(Reuse.Singleton);

            container.Register<ItemsConfig>(Reuse.Singleton);

            //Register VM
            container.Register<FinanceCalculateViewModel>(Reuse.Singleton);
            container.Register<NavigationTopViewModel>(Reuse.Singleton);
            container.Register<MainWindowViewModel>(Reuse.Singleton);
            container.Register<HistoryViewModel>(Reuse.Singleton);
            container.Register<MessageViewModel>(Reuse.Singleton);
            container.Register<SettingsViewModel>(Reuse.Singleton);
            container.Register<AboutViewModel>(Reuse.Singleton);
            container.Register<PlotViewModel>(Reuse.Singleton);

            var resolver = new DryIocDependencyResolver(container);
            Locator.SetLocator(resolver);
            container.RegisterInstance(resolver);

            resolver.InitializeSplat();
            resolver.InitializeReactiveUI();
            resolver.RegisterConstant(new AvaloniaActivationForViewFetcher(), typeof(IActivationForViewFetcher));
            resolver.RegisterConstant(new AutoDataTemplateBindingHook(), typeof(IPropertyBindingHook));
            RxApp.MainThreadScheduler = AvaloniaScheduler.Instance;

            Container = container;
        }

        public async void InitializeModules()
        {
            Console.WriteLine("Start initializing modules");

            var desktopServices = Container.ResolveMany<IDesktopAppService>();

            foreach (var service in desktopServices)
            {
                try
                {
                    await service.StartAsync();
                }
                catch (Exception ex)
                {
                }
            }
        }


        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var args = Environment.GetCommandLineArgs();
                Avalonia.Controls.WindowState state = Avalonia.Controls.WindowState.Maximized;
                if (args.FirstOrDefault(a => a.ToUpper().Contains("FULLSCREEN")) != default)
                {
                    state = Avalonia.Controls.WindowState.FullScreen;
                }

                var vm = Container?.Resolve<MainWindowViewModel>();
                desktop.MainWindow = Container.Resolve<MainWindow>();
                desktop.MainWindow.DataContext = vm;
                desktop.MainWindow.WindowState = state;
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
