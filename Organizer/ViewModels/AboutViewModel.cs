using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Organizer.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        public string Version
        {
            get
            {
                var v = Assembly.GetExecutingAssembly().GetName().Version;
                return $"Версия программы: {v.Major}.{v.Minor}.{v.Build}.{v.Revision}";
            }
        }
    }
}
