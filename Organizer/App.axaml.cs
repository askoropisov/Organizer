using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using DryIoc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Organizer.Infrastructure.Factories;
using Organizer.Infrastructure.Services;
using Organizer.Models;
using Organizer.Models.Configs;
using Organizer.Services;
using Organizer.ViewModels;
using Organizer.Views;
using ReactiveUI;
using Splat;
using Splat.DryIoc;
using System;
using System.IO;

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

            container.Register<ViewModelFactory>(Reuse.Singleton);

            container.Register<PathsService>(Reuse.Singleton);
            container.Register<ItemsService>(Reuse.Singleton);

            container.Register<ItemsConfig>(Reuse.Singleton);

            container.Register<PiePlot>(Reuse.Singleton);

            container.Register<MainWindowViewModel>(Reuse.Singleton);
            container.Register<AboutViewModel>();
            container.Register<FinanceCalculateViewModel>();
            container.Register<HistoryViewModel>();

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

        public void InitializeModules()
        {
            using var context = Container.Resolve<DataContext>();
            try
            {
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
            }
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = Container?.Resolve<MainWindowViewModel>(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
