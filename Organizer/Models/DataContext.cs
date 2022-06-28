using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Organizer.Infrastructure.Services;
using System;
using System.IO;

namespace Organizer.Models
{
    public class DataContext : DbContext
    {
        private readonly PathsService _paths;

        //public DataContext()
        //{
        //    _paths = new PathsService();
        //}

        public DataContext(PathsService paths)
        {
            _paths = paths;
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = new SqliteConnectionStringBuilder { DataSource = Path.Combine(_paths.DbDirectory, "data.db") }.ToString();
                var connection = new SqliteConnection(connectionString);
                optionsBuilder.UseSqlite(connection);
                optionsBuilder.LogTo(Console.WriteLine);
            }
        }
        public DbSet<RecordMonth> RecordMonths { get; set; }
    }
}
