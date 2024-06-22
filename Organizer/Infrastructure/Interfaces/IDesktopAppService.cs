using System.Threading.Tasks;

namespace Organizer.Infrastructure.Interfaces
{
    public interface IDesktopAppService
    {
        Task StartAsync();

        Task StopAsync();
    }

}
