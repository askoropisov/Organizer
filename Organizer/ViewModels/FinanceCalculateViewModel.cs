using Avalonia.Data;
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
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace Organizer.ViewModels
{

    public class FinanceCalculateViewModel : ViewModelBase
    {
        private readonly ItemsService _items;
        private readonly HistoryService _history;
        private readonly PathsService _path;
        private readonly PiePlot _plot;

        private readonly Regex regex = new Regex(@"\d+");

        public FinanceCalculateViewModel(ItemsService items,
                                         HistoryService history,
                                         PathsService path,
                                         PiePlot plot)
        {
            _items = items;
            _history = history;
            _path = path;
            _plot = plot;

            UpdatePlot();

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


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<double> Values { get; set; } = new ObservableCollection<double>();

        public ICommand SumEatCommand { get; }
        public ICommand SumTransportCommand { get; }
        public ICommand SumHomeCommand { get; }
        public ICommand SumServicesCommand { get; }
        public ICommand SumRelaxationCommand { get; }
        public ICommand SumOtherCommand { get; }
        public ICommand SumIncomeCommand { get; }

        public ICommand ClearDataCommand { get; }
        public ICommand SaveDatasCommand { get; }


        private Bitmap _currentImage;
        public Bitmap CurrentImage
        {
            get => _currentImage;
            set => this.RaiseAndSetIfChanged(ref _currentImage, value);
        }

        private int _resEat = 0;
        public int ResEat
        {
            get => _items.Eat;
            set
            {
                if (value >= 0)
                {
                    _items.Eat = value;
                    this.RaiseAndSetIfChanged(ref _resEat, value);
                    UpdatePlot();
                    OnPropertyChanged();
                }
            }
        }

        private int _resTransport = 0;
        public int ResTransport
        {
            get => _items.Transport;
            set
            {
                if (value >= 0)
                {
                    _items.Transport = value;
                    this.RaiseAndSetIfChanged(ref _resTransport, value);
                    UpdatePlot();
                    OnPropertyChanged();
                }
            }
        }

        private int _resHome = 0;
        public int ResHome
        {
            get => _items.Home;
            set
            {
                if (value >= 0)
                {
                    _items.Home = value;
                    this.RaiseAndSetIfChanged(ref _resHome, value);
                    UpdatePlot();
                    OnPropertyChanged();
                }
            }
        }

        private int _resServices = 0;
        public int ResServices
        {
            get => _items.Services;
            set
            {
                if (value >= 0)
                {
                    _items.Services = value;
                    this.RaiseAndSetIfChanged(ref _resServices, value);
                    UpdatePlot();
                    OnPropertyChanged();
                }
            }
        }

        private int _resRelax = 0;
        public int ResRelax
        {
            get => _items.Relaxation;
            set
            {
                if (value >= 0)
                {
                    _items.Relaxation = value;
                    this.RaiseAndSetIfChanged(ref _resRelax, value);
                    UpdatePlot();
                    OnPropertyChanged();
                }
            }
        }

        private int _resOther = 0;
        public int ResOther
        {
            get => _items.Other;
            set
            {
                if (value >= 0)
                {
                    _items.Other = value;
                    this.RaiseAndSetIfChanged(ref _resOther, value);
                    UpdatePlot();
                    OnPropertyChanged();
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
                    this.RaiseAndSetIfChanged(ref _resIncome, value);
                    Difference = ResIncome - TotalMoney;
                    OnPropertyChanged();
                }
            }
        }

        private int _eat = 0;
        public string Eat
        {
            get => _eat.ToString();
            set
            {
                MatchCollection matches = regex.Matches(value);
                if (matches.Count > 0)
                {
                    if (!int.TryParse(value, out int intValue))
                    {
                        throw new DataValidationException("Введите целое положительное число");
                    }

                    if (intValue < 0) throw new DataValidationException("Значение должно быть положительным");

                    this.RaiseAndSetIfChanged(ref _eat, intValue);
                }
                else
                {
                    throw new DataValidationException("Введите целое положительное число");
                }
            }
        }

        private int _transport = 0;
        public string Transport
        {
            get => _transport.ToString();
            set
            {
                MatchCollection matches = regex.Matches(value);
                if (matches.Count > 0)
                {
                    if (!int.TryParse(value, out int intValue))
                    {
                        throw new DataValidationException("Введите целое положительное число");
                    }

                    if (intValue < 0) throw new DataValidationException("Значение должно быть положительным");

                    this.RaiseAndSetIfChanged(ref _transport, intValue);
                }
                else
                {
                    throw new DataValidationException("Введите целое положительное число");
                }
            }
        }

        private int _home = 0;
        public string Home
        {
            get => _home.ToString();
            set
            {
                MatchCollection matches = regex.Matches(value);
                if (matches.Count > 0)
                {
                    if (!int.TryParse(value, out int intValue))
                    {
                        throw new DataValidationException("Введите целое положительное число");
                    }

                    if (intValue < 0) throw new DataValidationException("Значение должно быть положительным");

                    this.RaiseAndSetIfChanged(ref _home, intValue);
                }
                else
                {
                    throw new DataValidationException("Введите целое положительное число");
                }
            }
        }

        private int _services = 0;
        public string Services
        {
            get => _services.ToString();
            set
            {
                MatchCollection matches = regex.Matches(value);
                if (matches.Count > 0)
                {
                    if (!int.TryParse(value, out int intValue))
                    {
                        throw new DataValidationException("Введите целое положительное число");
                    }

                    if (intValue < 0) throw new DataValidationException("Значение должно быть положительным");

                    this.RaiseAndSetIfChanged(ref _services, intValue);
                }
                else
                {
                    throw new DataValidationException("Введите целое положительное число");
                }
            }
        }

        private int _relaxation = 0;
        public string Relaxation
        {
            get => _relaxation.ToString();
            set
            {
                MatchCollection matches = regex.Matches(value);
                if (matches.Count > 0)
                {
                    if (!int.TryParse(value, out int intValue))
                    {
                        throw new DataValidationException("Введите целое положительное число");
                    }

                    if (intValue < 0) throw new DataValidationException("Значение должно быть положительным");

                    this.RaiseAndSetIfChanged(ref _relaxation, intValue);
                }
                else
                {
                    throw new DataValidationException("Введите целое положительное число");
                }
            }
        }

        private int _other = 0;
        public string Other
        {
            get => _other.ToString();
            set
            {
                MatchCollection matches = regex.Matches(value);
                if (matches.Count > 0)
                {
                    if (!int.TryParse(value, out int intValue))
                    {
                        throw new DataValidationException("Введите целое положительное число");
                    }

                    if (intValue < 0) throw new DataValidationException("Значение должно быть положительным");

                    this.RaiseAndSetIfChanged(ref _other, intValue);
                }
                else
                {
                    throw new DataValidationException("Введите целое положительное число");
                }
            }
        }

        private int _totalMoney = 0;
        public int TotalMoney
        {
            get => _items.Total;
            set
            {
                if (value >= 0)
                {
                    _items.Total = value;
                    this.RaiseAndSetIfChanged(ref _totalMoney, value);
                    Difference = ResIncome - TotalMoney;
                }
            }
        }

        private int _income = 0;
        public string Income
        {
            get => _income.ToString();
            set
            {
                MatchCollection matches = regex.Matches(value);
                if (matches.Count > 0)
                {
                    if (!int.TryParse(value, out int intValue))
                    {
                        throw new DataValidationException("Введите целое положительное число");
                    }

                    if (intValue < 0) throw new DataValidationException("Значение должно быть положительным");

                    this.RaiseAndSetIfChanged(ref _income, intValue);
                }
                else
                {
                    throw new DataValidationException("Введите целое положительное число");
                }
            }
        }

        private int _diff;
        public int Difference
        {
            get => ResIncome-TotalMoney;
            set => this.RaiseAndSetIfChanged(ref _diff, value);
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
            ResEat += Convert.ToInt32(Eat);
            TotalMoney += Convert.ToInt32(Eat);
            Eat = "0";
        }

        public void SumTransport()
        {
            ResTransport += Convert.ToInt32(Transport);
            TotalMoney += Convert.ToInt32(Transport);
            Transport = "0";
        }

        public void SumHome()
        {
            ResHome += Convert.ToInt32(Home);
            TotalMoney += Convert.ToInt32(Home);
            Home = "0";
        }

        public void SumServices()
        {
            ResServices += Convert.ToInt32(Services);
            TotalMoney += Convert.ToInt32(Services);
            Services = "0";
        }

        public void SumRelaxation()
        {
            ResRelax += Convert.ToInt32(Relaxation);
            TotalMoney += Convert.ToInt32(Relaxation);
            Relaxation = "0";
        }

        public void SumOther()
        {
            ResOther += Convert.ToInt32(Other);
            TotalMoney += Convert.ToInt32(Other);
            Other = "0";
        }

        public void SumIncome()
        {
            ResIncome += Convert.ToInt32(Income);
            Income = "0";
        }


        /// <summary>
        /// TODO Корявая функция, надо переделать нормально и включить историю
        /// </summary>
        public void ClearData()
        {
            ResEat = 0;
            ResTransport = 0;
            ResHome = 0;
            ResServices = 0;
            ResRelax = 0;
            ResOther = 0;
            ResIncome = 0;
            TotalMoney = 0;
            OnPropertyChanged();
            _items.ClearTailJson("items.json");
        }

        public void UpdatePlot()
        {
            CollectionRefresh();
            _plot.UpdatePlot(Values.ToArray<double>());
            CurrentImage = new Bitmap(_path.ConfigDirectory + "/currentPie.png");
        }

        public void CollectionRefresh()
        {
            Values.Clear();
            Values.Add(ResEat);
            Values.Add(ResTransport);
            Values.Add(ResHome);
            Values.Add(ResServices);
            Values.Add(ResRelax);
            Values.Add(ResOther);
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
