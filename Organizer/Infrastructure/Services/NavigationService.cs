using Organizer.Infrastructure.Factories;
using Organizer.Infrastructure.Interfaces;
using Organizer.ViewModels;
using System;
using System.Threading.Tasks;

namespace Organizer.Infrastructure.Services
{
    public class NavigationService : IDesktopAppService
    {
        public event EventHandler<ViewModelBase>? PageChanged;

        private readonly ViewModelFactory _viewModelFactory;

        public NavigationService(ViewModelFactory viewModelFactory)
        {
            Console.WriteLine("Navigation service created");
            _viewModelFactory = viewModelFactory;
        }

        private ViewModelBase? _currentViewModel;
        public ViewModelBase? CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                if (value != null)
                {
                    PageChanged?.Invoke(this, value);
                }
            }
        }

        public void NavigateTo<T>() where T : ViewModelBase
        {
            if (CurrentViewModel is INavigationPage fromPage)
            {
                fromPage.OnNavigatedFrom();
            }

            var vm = _viewModelFactory.Create<T>();

            if (vm == null) return;

            if (vm is INavigationPage toPage)
            {
                toPage.OnNavigatedTo();
            }

            CurrentViewModel = vm;
        }

        public void NavigateTo(ViewModelBase page)
        {
            if (CurrentViewModel is INavigationPage fromPage)
            {
                fromPage.OnNavigatedFrom();
            }

            if (page is INavigationPage toPage)
            {
                toPage.OnNavigatedTo();
            }

            CurrentViewModel = page;
        }

        public Task StartAsync()
        {
            return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            return Task.CompletedTask;
        }
    }

}
