using Avalonia.Media.Imaging;
using Microsoft.EntityFrameworkCore;
using Organizer.Infrastructure;
using Organizer.Infrastructure.Interfaces;
using Organizer.Infrastructure.Services;
using Organizer.Models;
using Organizer.Services;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Organizer.ViewModels
{

    public class FinanceCalculateViewModel : ViewModelBase, INavigationPage
    {
        private readonly Func<DataContext> _dataContextFactory;
        private readonly ItemsService _items;
        private readonly PathsService _path;
        private readonly PiePlot _plot;

        private readonly Regex regex = new Regex(@"\d+");

        public FinanceCalculateViewModel(Func<DataContext> dataContextFactory,
                                         ItemsService items,
                                         PathsService path,
                                         PiePlot plot)
        {
            _dataContextFactory = dataContextFactory;
            _items = items;
            _path = path;
            _plot = plot;

            //Init();


            //CurrentImage = new Bitmap(_path.ConfigDirectory + "/currentPie.png");

        }

        ObservableCollection<string> Categories = new ObservableCollection<string>();

        private string _selCat;
        public string SelectedCategory
        {
            get => _selCat;
            set
            {
                this.RaiseAndSetIfChanged(ref _selCat, value);
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
                                  select new Category(cat.Name))
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
        public async void SaveDatasAsync()
        {

        }

        public void OnNavigatedFrom()
        {

        }

        public void OnNavigatedTo()
        {
            Init();
        }
    }
}
