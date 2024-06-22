using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Organizer.Infrastructure.Services;
using Organizer.Models;
using System.IO;

namespace Organizer.Infrastructure
{
    public class DataContext : DbContext
    {
        private readonly PathsService _paths;

        public DataContext(PathsService paths)
        {
            _paths = paths;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = new SqliteConnectionStringBuilder { DataSource = Path.Combine(_paths.DbDirectory, "data.db") }.ToString();
                var connection = new SqliteConnection(connectionString);
                optionsBuilder.UseSqlite(connection);
            }
        }

        public DbSet<Expense> Expenses => Set<Expense>();
        public DbSet<Category> Categories => Set<Category>();
    }

}
