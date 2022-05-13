using Organizer.Models;
using Organizer.Services;
using Prism.Commands;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Organizer.ViewModels
{
    public class FinanceCalculateViewModel : ViewModelBase
    {
        private readonly ItemsService _items;
        private readonly HistoryService _history;
        //private readonly History _history;

        public FinanceCalculateViewModel()
        {
            _items = ItemsService.Instance;
            _history = HistoryService.Instance;
            // _history = History.Instance;

            SumEatCommand = new DelegateCommand(SumEat);
            SumTransportCommand = new DelegateCommand(SumTransport);
            SumHomeCommand = new DelegateCommand(SumHome);
            SumServicesCommand = new DelegateCommand(SumServices);
            SumRelaxationCommand = new DelegateCommand(SumRelaxation);
            SumOtherCommand = new DelegateCommand(SumOther);
            ClearDataCommand = new DelegateCommand(ClearData);
            SaveDatasCommand = new DelegateCommand(SaveDatas);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand SumEatCommand { get; }
        public ICommand SumTransportCommand { get; }
        public ICommand SumHomeCommand { get; }
        public ICommand SumServicesCommand { get; }
        public ICommand SumRelaxationCommand { get; }
        public ICommand SumOtherCommand { get; }

        public ICommand ClearDataCommand { get; }
        public ICommand SaveDatasCommand { get; }

        private double _res1 = 0;
        public double Res1
        {
            get => _items.Eat;
            set
            {
                if (value >= 0)
                {
                    _items.Eat = value;
                    OnPropertyChanged();
                    this.RaiseAndSetIfChanged(ref _res1, value);
                }
            }
        }

        private double _res2 = 0;
        public double Res2
        {
            get => _items.Transport;
            set
            {
                if (value >= 0)
                {
                    _items.Transport = value;
                    OnPropertyChanged();
                    this.RaiseAndSetIfChanged(ref _res2, value);
                }
            }
        }

        private double _res3 = 0;
        public double Res3
        {
            get => _items.Home;
            set
            {
                if (value >= 0)
                {
                    _items.Home = value;
                    OnPropertyChanged();
                    this.RaiseAndSetIfChanged(ref _res3, value);
                }
            }
        }

        private double _res4 = 0;
        public double Res4
        {
            get => _items.Services;
            set
            {
                if (value >= 0)
                {
                    _items.Services = value;
                    OnPropertyChanged();
                    this.RaiseAndSetIfChanged(ref _res4, value);
                }
            }
        }

        private double _res5 = 0;
        public double Res5
        {
            get => _items.Relaxation;
            set
            {
                if (value >= 0)
                {
                    _items.Relaxation = value;
                    OnPropertyChanged();
                    this.RaiseAndSetIfChanged(ref _res5, value);
                }
            }
        }

        private double _res6 = 0;
        public double Res6
        {
            get => _items.Other;
            set
            {
                if (value >= 0)
                {
                    _items.Other = value;
                    OnPropertyChanged();
                    this.RaiseAndSetIfChanged(ref _res6, value);
                }
            }
        }

        private double _eat = 0;
        public double Eat
        {
            get => _eat;
            set
            {
                if (value >= 0)
                    this.RaiseAndSetIfChanged(ref _eat, value);
            }
        }

        private double _transport = 0;
        public double Transport
        {
            get => _transport;
            set
            {
                if (value >= 0)
                    this.RaiseAndSetIfChanged(ref _transport, value);
            }
        }

        private double _home = 0;
        public double Home
        {
            get => _home;
            set
            {
                if (value >= 0)
                    this.RaiseAndSetIfChanged(ref _home, value);
            }
        }

        private double _services = 0;
        public double Services
        {
            get => _services;
            set
            {
                if (value >= 0)
                    this.RaiseAndSetIfChanged(ref _services, value);
            }
        }

        private double _relaxation = 0;
        public double Relaxation
        {
            get => _relaxation;
            set
            {
                if (value >= 0)
                    this.RaiseAndSetIfChanged(ref _relaxation, value);
            }
        }

        private double _other = 0;
        public double Other
        {
            get => _other;
            set
            {
                if (value >= 0)
                    this.RaiseAndSetIfChanged(ref _other, value);
            }
        }

        private double _totalMoney = 0;
        public double TotalMoney
        {
            get => _items.Total;
            set
            {
                if (value >= 0)
                {
                    _items.Total = value;
                    this.RaiseAndSetIfChanged(ref _totalMoney, value);
                }
            }
        }

        private string _nowTime = DateTime.Now.ToShortDateString();
        public string NowTime
        {
            get => _nowTime;
            set
            {
                this.RaiseAndSetIfChanged(ref _nowTime, value);
            }
        }

        public void SumEat()
        {
            Res1 += Eat;
            TotalMoney += Eat;
            Eat = 0;
        }

        public void SumTransport()
        {
            Res2 += Transport;
            TotalMoney += Transport;
            Transport = 0;
        }

        public void SumHome()
        {
            Res3 += Home;
            TotalMoney += Home;
            Home = 0;
        }

        public void SumServices()
        {
            Res4 += Services;
            TotalMoney += Services;
            Services = 0;
        }

        public void SumRelaxation()
        {
            Res5 += Relaxation;
            TotalMoney += Relaxation;
            Relaxation = 0;
        }

        public void SumOther()
        {
            Res6 += Other;
            TotalMoney += Other;
            Other = 0;
        }


        /// <summary>
        /// TODO Корявая функция, надо переделать нормально и включить историю
        /// </summary>
        public void ClearData()
        {
            _items.ClearData();
            Res1 = 0;
            Res2 = 0;
            Res3 = 0;
            Res4 = 0;
            Res5 = 0;
            Res6 = 0;
            TotalMoney = 0;
            OnPropertyChanged();
        }


        /// <summary>
        /// явно сделано слишком сложно
        /// </summary>
        public void SaveDatas()
        {
            try
            {
                string? last_date = _history.CollectionHistory.Last().Data;
                string dateNow = DateTime.Now.Month.ToString() + " " + DateTime.Now.Year.ToString();

                if (dateNow == last_date)
                {
                    ObservableCollection<ScoreForMonth> NewHistory = _history.CollectionHistory;
                    ScoreForMonth ThisMonth = new ScoreForMonth();

                    NewHistory.Last().Eat = Res1;
                    NewHistory.Last().Transport = Res2;
                    NewHistory.Last().Home = Res3;
                    NewHistory.Last().Services = Res4;
                    NewHistory.Last().Relaxation = Res5;
                    NewHistory.Last().Other = Res6;
                    NewHistory.Last().Total = TotalMoney;
                    NewHistory.Last().Data = dateNow;

                    _history.CollectionHistory = NewHistory;
                    OnPropertyChanged();
                }
                else
                {
                    ObservableCollection<ScoreForMonth> NewHistory = _history.CollectionHistory;
                    ScoreForMonth ThisMonth = new ScoreForMonth();

                    ThisMonth.Eat = Res1;
                    ThisMonth.Transport = Res2;
                    ThisMonth.Home = Res3;
                    ThisMonth.Services = Res4;
                    ThisMonth.Relaxation = Res5;
                    ThisMonth.Other = Res6;
                    ThisMonth.Total = TotalMoney;
                    ThisMonth.Data = dateNow;

                    NewHistory.Add(ThisMonth);
                    _history.CollectionHistory = NewHistory;
                    OnPropertyChanged();
                }
            }
            catch
            {
                string dateNow = DateTime.Now.Month.ToString() + " " + DateTime.Now.Year.ToString();
                ScoreForMonth ThisMonth = new ScoreForMonth();

                ObservableCollection<ScoreForMonth> NewHistory = new ObservableCollection<ScoreForMonth>();

                ThisMonth.Eat = Res1;
                ThisMonth.Transport = Res2;
                ThisMonth.Home = Res3;
                ThisMonth.Services = Res4;
                ThisMonth.Relaxation = Res5;
                ThisMonth.Other = Res6;
                ThisMonth.Total = TotalMoney;
                ThisMonth.Data = dateNow;

                NewHistory.Add(ThisMonth);
                _history.CollectionHistory = NewHistory;
                OnPropertyChanged();
            }
        }
    }
}
