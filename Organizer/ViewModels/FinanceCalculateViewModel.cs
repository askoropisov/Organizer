using Avalonia.Media.Imaging;
using Organizer.Infrastructure.Services;
using Organizer.Models;
using Organizer.Services;
using OxyPlot;
using OxyPlot.Series;
using Prism.Commands;
using ReactiveUI;
using ScottPlot.Avalonia;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Organizer.ViewModels
{

    public class FinanceCalculateViewModel : ViewModelBase
    {
        private readonly ItemsService _items;
        private readonly HistoryService _history;
        private readonly PathsService _path;
        private readonly PiePlot _plot;

        public FinanceCalculateViewModel(ItemsService items,
                                         HistoryService history,
                                         PathsService path,
                                         PiePlot plot)
        {
            _items = items;
            _history = history;
            _path = path;
            _plot = plot;

            CurrentImage = new Bitmap(_path.ConfigDirectory + "/currentPie.png");

            SumEatCommand = new DelegateCommand(SumEat);
            SumTransportCommand = new DelegateCommand(SumTransport);
            SumHomeCommand = new DelegateCommand(SumHome);
            SumServicesCommand = new DelegateCommand(SumServices);
            SumRelaxationCommand = new DelegateCommand(SumRelaxation);
            SumOtherCommand = new DelegateCommand(SumOther);
            SumIncomeCommand = new DelegateCommand(SumIncome);
            ClearDataCommand = new DelegateCommand(ClearData);
            SaveDatasCommand = new DelegateCommand(SaveDatas);

        }

        private Bitmap _currentImage;
        public Bitmap CurrentImage
        {
            get => _currentImage;
            set => this.RaiseAndSetIfChanged(ref _currentImage, value);
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
        public ICommand SumIncomeCommand { get; }

        public ICommand ClearDataCommand { get; }
        public ICommand SaveDatasCommand { get; }

        public void UpdatePlot()
        {
            _plot.UpdatePlot();
            CurrentImage = new Bitmap(_path.ConfigDirectory + "/currentPie.png");
        }

        private double _resEat = 0;
        public double ResEat
        {
            get => _items.Eat;
            set
            {
                if (value >= 0)
                {
                    _items.Eat = value;
                    OnPropertyChanged();
                    this.RaiseAndSetIfChanged(ref _resEat, value);
                }
            }
        }

        private double _resTransport = 0;
        public double ResTransport
        {
            get => _items.Transport;
            set
            {
                if (value >= 0)
                {
                    _items.Transport = value;
                    OnPropertyChanged();
                    this.RaiseAndSetIfChanged(ref _resTransport, value);
                }
            }
        }

        private double _resHome = 0;
        public double ResHome
        {
            get => _items.Home;
            set
            {
                if (value >= 0)
                {
                    _items.Home = value;
                    OnPropertyChanged();
                    this.RaiseAndSetIfChanged(ref _resHome, value);
                }
            }
        }

        private double _resServices = 0;
        public double ResServices
        {
            get => _items.Services;
            set
            {
                if (value >= 0)
                {
                    _items.Services = value;
                    OnPropertyChanged();
                    this.RaiseAndSetIfChanged(ref _resServices, value);
                }
            }
        }

        private double _resRelax = 0;
        public double ResRelax
        {
            get => _items.Relaxation;
            set
            {
                if (value >= 0)
                {
                    _items.Relaxation = value;
                    OnPropertyChanged();
                    this.RaiseAndSetIfChanged(ref _resRelax, value);
                }
            }
        }

        private double _resOther = 0;
        public double ResOther
        {
            get => _items.Other;
            set
            {
                if (value >= 0)
                {
                    _items.Other = value;
                    OnPropertyChanged();
                    this.RaiseAndSetIfChanged(ref _resOther, value);
                }
            }
        }

        private int _resIncome = 0;
        public int ResIncome
        {
            get => _items.Income;
            set
            {
                if (value >= 0)
                {
                    _items.Income = value;
                    OnPropertyChanged();
                    this.RaiseAndSetIfChanged(ref _resIncome, value);
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

        private int _income = 0;
        public int Income
        {
            get => _income;
            set
            {
                if(value >= 0)
                    this.RaiseAndSetIfChanged(ref _income, value);
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
            ResEat += Eat;
            TotalMoney += Eat;
            Eat = 0;
        }

        public void SumTransport()
        {
            ResTransport += Transport;
            TotalMoney += Transport;
            Transport = 0;
        }

        public void SumHome()
        {
            ResHome += Home;
            TotalMoney += Home;
            Home = 0;
        }

        public void SumServices()
        {
            ResServices += Services;
            TotalMoney += Services;
            Services = 0;
        }

        public void SumRelaxation()
        {
            ResRelax += Relaxation;
            TotalMoney += Relaxation;
            Relaxation = 0;
        }

        public void SumOther()
        {
            ResOther += Other;
            TotalMoney += Other;
            Other = 0;
        }

        public void SumIncome()
        {
            ResIncome += Income;
            Income = 0;
        }


        /// <summary>
        /// TODO Корявая функция, надо переделать нормально и включить историю
        /// </summary>
        public void ClearData()
        {
            _items.ClearData();
            ResEat = 0;
            ResTransport = 0;
            ResHome = 0;
            ResServices = 0;
            ResRelax = 0;
            ResOther = 0;
            ResIncome = 0;
            TotalMoney = 0;
            OnPropertyChanged();
        }


        /// <summary>
        /// явно сделано слишком сложно
        /// </summary>
        public void SaveDatas()
        {
        //    try
        //    {
        //        string? last_date = _history.CollectionHistory.Last().Data;
        //        string dateNow = DateTime.Now.Month.ToString() + " " + DateTime.Now.Year.ToString();

        //        if (dateNow == last_date)
        //        {
        //            ObservableCollection<ScoreForMonth> NewHistory = _history.CollectionHistory;
        //            ScoreForMonth ThisMonth = new ScoreForMonth();

        //            NewHistory.Last().Eat = ResEat;
        //            NewHistory.Last().Transport = ResTransport;
        //            NewHistory.Last().Home = ResHome;
        //            NewHistory.Last().Services = ResServices;
        //            NewHistory.Last().Relaxation = ResRelax;
        //            NewHistory.Last().Other = ResOther;
        //            NewHistory.Last().Total = TotalMoney;
        //            NewHistory.Last().Data = dateNow;

        //            _history.CollectionHistory = NewHistory;
        //            OnPropertyChanged();
        //        }
        //        else
        //        {
        //            ObservableCollection<ScoreForMonth> NewHistory = _history.CollectionHistory;
        //            ScoreForMonth ThisMonth = new ScoreForMonth();

        //            ThisMonth.Eat = ResEat;
        //            ThisMonth.Transport = ResTransport;
        //            ThisMonth.Home = ResHome;
        //            ThisMonth.Services = ResServices;
        //            ThisMonth.Relaxation = ResRelax;
        //            ThisMonth.Other = ResOther;
        //            ThisMonth.Total = TotalMoney;
        //            ThisMonth.Data = dateNow;

        //            NewHistory.Add(ThisMonth);
        //            _history.CollectionHistory = NewHistory;
        //            OnPropertyChanged();
        //        }
        //    }
        //    catch
        //    {
        //        string dateNow = DateTime.Now.Month.ToString() + " " + DateTime.Now.Year.ToString();
        //        ScoreForMonth ThisMonth = new ScoreForMonth();

        //        ObservableCollection<ScoreForMonth> NewHistory = new ObservableCollection<ScoreForMonth>();

        //        ThisMonth.Eat = ResEat;
        //        ThisMonth.Transport = ResTransport;
        //        ThisMonth.Home = ResHome;
        //        ThisMonth.Services = ResServices;
        //        ThisMonth.Relaxation = ResRelax;
        //        ThisMonth.Other = ResOther;
        //        ThisMonth.Total = TotalMoney;
        //        ThisMonth.Data = dateNow;

        //        NewHistory.Add(ThisMonth);
        //        _history.CollectionHistory = NewHistory;
        //        OnPropertyChanged();
        //    }
        }
    }
}
