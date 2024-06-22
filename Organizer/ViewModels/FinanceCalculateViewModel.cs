using Avalonia.Data;
using Avalonia.Media.Imaging;
using Microsoft.EntityFrameworkCore;
using Organizer.Infrastructure;
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
using System.Threading.Tasks;
using System.Windows.Input;

namespace Organizer.ViewModels
{

    public class FinanceCalculateViewModel : ViewModelBase
    {
        private readonly Func<DataContext> _dataContextFactory;
        private readonly ItemsService _items;
        private readonly HistoryService _history;
        private readonly PathsService _path;
        private readonly PiePlot _plot;

        private readonly Regex regex = new Regex(@"\d+");

        public FinanceCalculateViewModel(Func<DataContext> dataContextFactory,
                                         ItemsService items,
                                         HistoryService history,
                                         PathsService path,
                                         PiePlot plot)
        {
            _dataContextFactory = dataContextFactory;
            _items = items;
            _history = history;
            _path = path;
            _plot = plot;

            Init();


            //CurrentImage = new Bitmap(_path.ConfigDirectory + "/currentPie.png");

        }

        ObservableCollection<string> Categories = new ObservableCollection<string>();

        private string _selCat;
        public string SelectedCategory
        {
            get => _selCat;
            set
            {
                this.RaiseAndSetIfChanged(ref  _selCat, value);
            }
        }

        private int _value = 0;
        public int Value
        {
            get => _value;
            set
            {
                this.RaiseAndSetIfChanged(ref _value, value);
            }
        }

        private int _summ;
        public int Summ
        {
            get => _summ;
            set
            {
                this.RaiseAndSetIfChanged(ref _summ, value);
            }
        }

        private string? _discripton;
        public string? Description
        {
            get => _discripton;
            set
            {
                this.RaiseAndSetIfChanged(ref _discripton, value);
            }
        }

        public async Task AddValue()
        {
            Expense expense = new Expense();
            if (SelectedCategory != null && SelectedCategory != string.Empty)
            {
                expense.Category = SelectedCategory;
                expense.Description = Description;
                expense.Value = Value;
            }
        }



        private Bitmap _currentImage;
        public Bitmap CurrentImage
        {
            get => _currentImage;
            set => this.RaiseAndSetIfChanged(ref _currentImage, value);
        }

        public async Task Init()
        {
            using var context = _dataContextFactory();
            int CountRec = context.Categories.Count();
            var recItems = await (from cat in context.Categories
                                  select new Category(cat.Id, cat.Name))
                            .Take(CountRec)
                            .ToListAsync();

            Categories.Clear();

            foreach (var item in recItems)
            {
                Categories.Add(item.Name);
            }
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
