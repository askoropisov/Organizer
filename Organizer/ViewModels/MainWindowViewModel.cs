using Organizer.Infrastructure.Factories;
using Organizer.Infrastructure.Services;
using Organizer.ViewModels.Controlls;
using ReactiveUI;

namespace Organizer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly NavigationService _navigationService;
        private readonly ViewModelFactory _viewModelFactory;

        public MainWindowViewModel(NavigationService navigation,
                                   ViewModelFactory viewModelFactory)
        {
            _navigationService = navigation;
            _viewModelFactory = viewModelFactory;

            _navigationService.PageChanged += OnPageChanged;
            _navigationService.NavigateTo<FinanceCalculateViewModel>();
            NavigationTop = _viewModelFactory.Create<NavigationTopViewModel>();

        }

        public NavigationTopViewModel NavigationTop { get; }

        private ViewModelBase? _currentPage;
        public ViewModelBase? CurrentPage
        {
            get => _currentPage;
            set => this.RaiseAndSetIfChanged(ref _currentPage, value);
        }

        private void OnPageChanged(object? sender, ViewModelBase e)
        {
            CurrentPage = e;
        }
    }
}
