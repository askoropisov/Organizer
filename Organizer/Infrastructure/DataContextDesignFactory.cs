using Microsoft.EntityFrameworkCore.Design;
using Organizer.Infrastructure.Services;

namespace Organizer.Infrastructure
{
    public class DataContextDesignFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            PathsService pathsService = new PathsService();

            return new DataContext(pathsService);
        }
    }

}
