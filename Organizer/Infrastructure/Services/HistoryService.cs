using Organizer.Infrastructure.Services;
using Organizer.Models;
using Organizer.Models.Configs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Organizer.Services
{
    public class HistoryService : INotifyPropertyChanged
    {
        History? _history;

        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly PathsService _paths;
        private readonly HistoryConfig _conf;

        public HistoryService(PathsService paths,
                              HistoryConfig config)
        {
            _paths = paths;
            _conf = config;

            _history = config.History;
        }


        public History History
        {
            get => _history;
            set
            {
                _history = value;
                OnPropertyChanged();
            }
        }


        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            _conf.WriteJSON(History);
        }
    }
}
