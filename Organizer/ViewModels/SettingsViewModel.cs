using DryIoc;
using Microsoft.EntityFrameworkCore;
using Organizer.Infrastructure;
using Organizer.Infrastructure.Interfaces;
using Organizer.Infrastructure.Services;
using Organizer.Models;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Organizer.ViewModels
{
    public class SettingsViewModel : ViewModelBase, INavigationPage
    {
        private readonly Func<DataContext> _dataContextFactory;
        private readonly SynchronizationContext _contextUI;
        private readonly DialogService _dialogService;


        public SettingsViewModel(Func<DataContext> dataContextFactory,
                                 DialogService dialogService)
        {
            _dataContextFactory = dataContextFactory;
            _dialogService = dialogService;
            _contextUI = SynchronizationContext.Current ?? throw new NullReferenceException();
        }

        public ObservableCollection<string> Categories { get; set; } = new ObservableCollection<string>();

        private string? _newItem;
        public string? NewItem
        {
            get => _newItem;
            set
            {
                this.RaiseAndSetIfChanged(ref _newItem, value);
            }
        }

        public async Task AddCategory()
        {
            Category cat = new Category(NewItem);

            using var db = _dataContextFactory();
            var findCat = db.Categories.FirstOrDefault(category => category.Name == cat.Name);

            if (findCat != default)
            {
                _contextUI.Post(_ =>
                _dialogService.ShowMessage("Такая категория уже существует", "Укажите другие данные"), null);
                NewItem = string.Empty;
                return;
            }

            if (!string.IsNullOrEmpty(NewItem))
            {
                db.Categories.Add(cat);
                db.SaveChanges();

                NewItem = string.Empty;
                await Init();
            }
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

        public void OnNavigatedFrom()
        {

        }

        public void OnNavigatedTo()
        {
            Init();
        }
    }
}
