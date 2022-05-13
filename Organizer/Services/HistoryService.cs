using Organizer.Models;
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
        public static HistoryService Instance { get; } = new HistoryService();
        History? _history;

        public event PropertyChangedEventHandler? PropertyChanged;

        public HistoryService()
        {
            Start();
        }

        public void Start()
        {
            if (_history == null)
            {
                History? history = null;
                try
                {
                    history = FileHistoryJSON.ReadJSON();
                }
                catch (JsonException)
                {
                }
                catch (UnauthorizedAccessException)
                { }

                if (history == null)
                {
                    history = new History();
                    FileHistoryJSON.WriteJSON(history);
                }

                History = history;


                foreach (var property in typeof(HistoryService).GetProperties())
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property.Name));
                }
            }
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

        public ObservableCollection<ScoreForMonth> CollectionHistory
        {
            get => History.history;
            set
            {
                History.history = value;
                OnPropertyChanged();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            FileHistoryJSON.WriteJSON(History);
        }
    }
}
