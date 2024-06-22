using Microsoft.EntityFrameworkCore;
using Organizer.Infrastructure.Interfaces;
using System;
using System.Threading.Tasks;

namespace Organizer.Infrastructure.Services
{
    public class DatabaseService : IDesktopAppService
    {
        private readonly Func<DataContext> _dataContextFactory;

        public DatabaseService(Func<DataContext> dataContextFactory)
        {
            _dataContextFactory = dataContextFactory;
        }

        public async Task StartAsync()
        {
            using var context = _dataContextFactory();

            try
            {
                await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            { }
        }

        public Task StopAsync()
        {
            return Task.CompletedTask;
        }
    }

}
