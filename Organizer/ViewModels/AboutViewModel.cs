using Organizer.Infrastructure.Interfaces;
using System.Reflection;

namespace Organizer.ViewModels
{
    public class AboutViewModel : ViewModelBase, INavigationPage
    {
        public string Version
        {
            get
            {
                var v = Assembly.GetExecutingAssembly().GetName().Version;
                return $"Версия программы: {v.Major}.{v.Minor}.{v.Build}.{v.Revision}";
            }
        }

        public void OnNavigatedFrom()
        {

        }

        public void OnNavigatedTo()
        {

        }
    }
}
