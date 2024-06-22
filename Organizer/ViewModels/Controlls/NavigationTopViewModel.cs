using Organizer.Infrastructure.Services;
using ReactiveUI;
using System;

namespace Organizer.ViewModels.Controlls
{
    public class NavigationTopViewModel : ViewModelBase
    {
        //private readonly Timer _timer;
        private readonly NavigationService _navigationService;
        //private readonly DialogService _dialogService;
        private readonly PathsService _pathsService;

        public NavigationTopViewModel(NavigationService navigationService,
                                      //DialogService dialogService,
                                      PathsService pathsService)
        {
            _navigationService = navigationService;
            //_dialogService = dialogService;

            //_timer = new Timer(UpdateTime, null, 1000, 1000);
            _pathsService = pathsService;
        }

        bool IsMain { get; set; } = true;
        bool IsInfo { get; set; }
        bool IsHistory { get; set; }
        bool IsSettings { get; set; }

        public DateTime CurrentTime => DateTime.Now;

        public void GoMain()
        {
            _navigationService.NavigateTo<FinanceCalculateViewModel>();
            IsMain = true;
            IsInfo = false;
            IsHistory = false;
            IsSettings = false;
            UpdateCurentPageColor();
        }

        public void GoSettings()
        {
            _navigationService.NavigateTo<SettingsViewModel>();
            IsMain = false;
            IsInfo = false;
            IsHistory = false;
            IsSettings = true;
            UpdateCurentPageColor();
        }

        public void GoHistory()
        {
            _navigationService.NavigateTo<HistoryViewModel>();
            IsMain = false;
            IsInfo = false;
            IsHistory = true;
            IsSettings = false;
            UpdateCurentPageColor();
        }

        public void GoInfo()
        {
            _navigationService.NavigateTo<AboutViewModel>();
            IsMain = false;
            IsInfo = true;
            IsHistory = false;
            IsSettings = false;
            UpdateCurentPageColor();
        }

        public void UpdateCurentPageColor()
        {
            this.RaisePropertyChanged(nameof(IsMain));
            this.RaisePropertyChanged(nameof(IsInfo));
            this.RaisePropertyChanged(nameof(IsHistory));
            this.RaisePropertyChanged(nameof(IsSettings));
        }

        private void UpdateTime(object? obj)
        {
            this.RaisePropertyChanged(nameof(CurrentTime));
        }

    }

}
