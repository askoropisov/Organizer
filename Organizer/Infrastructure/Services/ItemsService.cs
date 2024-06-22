using Organizer.Infrastructure.Services;
using Organizer.Models;
using Organizer.Models.Configs;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace Organizer.Services
{
    public class ItemsService : INotifyPropertyChanged
    {
        private readonly PathsService _paths;
        private readonly ItemsConfig _conf;
        Items _items;


        public event PropertyChangedEventHandler? PropertyChanged;

        public ItemsService(PathsService paths,
                            ItemsConfig conf)
        {
            _paths = paths;
            _conf = conf;

            _items = conf.Items;
        }


        public Items Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        public int Eat
        {
            get => Items.Eat;
            set
            {
                Items.Eat = value;
                OnPropertyChanged();
            }
        }

        public int Home
        {
            get => Items.Home;
            set
            {
                Items.Home = value;
                OnPropertyChanged();
            }
        }

        public int Services
        {
            get => Items.Services;
            set
            {
                Items.Services = value;
                OnPropertyChanged();
            }
        }

        public int Relaxation
        {
            get => Items.Relaxation;
            set
            {
                Items.Relaxation = value;
                OnPropertyChanged();
            }
        }

        public int Transport
        {
            get => Items.Transport;
            set
            {
                Items.Transport = value;
                OnPropertyChanged();
            }
        }

        public int Other
        {
            get => Items.Other;
            set
            {
                Items.Other = value;
                OnPropertyChanged();
            }
        }

        public int Income
        {
            get => Items.Income;
            set
            {
                Items.Income = value;
                OnPropertyChanged();
            }
        }

        public int Total
        {
            get => Items.Total;
            set
            {
                Items.Total = value;
                OnPropertyChanged();
            }
        }

        public void ClearData()
        {
            Items.Eat = 0;
            Items.Home = 0;
            Items.Transport = 0;
            Items.Services = 0;
            Items.Relaxation = 0;
            Items.Other = 0;
            Items.Total = 0;
            Items.Income = 0;
            //OnPropertyChanged();
        }


        //Обрезает лишние закрывающие скобки в JSON файле
        public void ClearTailJson(string nameFile)
        {
            char symbol = '}';
            string path = Path.Combine(_paths.ConfigDirectory, nameFile).ToString();

            FileInfo fileInf = new FileInfo(path);
            string fileJSON = File.ReadAllText(path);

            int index = fileJSON.IndexOf(symbol);
            fileJSON = fileJSON.Substring(0, index + 1);

            File.WriteAllText(path, fileJSON);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            _conf.WriteJSON(Items);
        }
    }
}
