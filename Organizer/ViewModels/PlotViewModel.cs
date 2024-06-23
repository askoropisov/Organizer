using DynamicData;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using Microsoft.EntityFrameworkCore;
using Organizer.Infrastructure;
using Organizer.Models;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace Organizer.ViewModels
{
    public class PlotViewModel : ViewModelBase
    {
        private readonly Func<DataContext> _dataContextFactory;

        public PlotViewModel(Func<DataContext> dataContextFactory)
        {
            _dataContextFactory = dataContextFactory;
        }

        public ISeries[] Series { get; set; } = new ISeries[0];


        public async Task Update()
        {
            using var context = _dataContextFactory();
            int CategoriesCount = context.Categories.Count();
            int ExpensesCount = context.Expenses.Count();

            var recItems = await (from cat in context.Categories
                                  select new Category(cat.Name))
                                  .Take(CategoriesCount)
                                  .ToListAsync();


            foreach (var category in recItems) 
            {
                int summ = 0;
                var exps = await (from exp in context.Expenses
                                  where exp.Date.Year == DateTime.Now.Year && exp.Date.Month == DateTime.Now.Month && exp.Category == category.Name
                                  select new Expense(exp.Category, exp.Description, exp.Value, exp.Date))
                              .Take(ExpensesCount)
                              .ToListAsync();

                foreach(var item in exps)
                {
                    summ += item.Value;
                }

                ISeries part = new PieSeries<int> { Values = new int[] {summ} };
                Series.Add((System.Collections.Generic.IEnumerable<ISeries>)part);
            }
        }
    }
}
