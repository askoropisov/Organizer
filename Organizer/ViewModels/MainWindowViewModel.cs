using Organizer.Infrastructure.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Organizer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ViewModelFactory _vmFactory;

        public MainWindowViewModel(ViewModelFactory vmFactory)
        {
            _vmFactory = vmFactory;

            FCDataContext = _vmFactory.Create<FinanceCalculateViewModel>();
            HistoryDataContext = _vmFactory.Create<HistoryViewModel>();
            AboutDataContext = _vmFactory.Create<AboutViewModel>();
        }


        public FinanceCalculateViewModel FCDataContext { get; }
        public HistoryViewModel HistoryDataContext { get; }
        public AboutViewModel AboutDataContext { get; }
    }
}
