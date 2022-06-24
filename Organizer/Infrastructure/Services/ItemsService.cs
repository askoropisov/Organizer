using Organizer.Infrastructure.Services;
using Organizer.Models;
using Organizer.Models.Configs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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

        public double Eat
        {
            get => Items.Eat;
            set
            {
                Items.Eat = value;
                OnPropertyChanged();
            }
        }

        public double Home
        {
            get => Items.Home;
            set
            {
                Items.Home = value;
                OnPropertyChanged();
            }
        }

        public double Services
        {
            get => Items.Services;
            set
            {
                Items.Services = value;
                OnPropertyChanged();
            }
        }

        public double Relaxation
        {
            get => Items.Relaxation;
            set
            {
                Items.Relaxation = value;
                OnPropertyChanged();
            }
        }

        public double Transport
        {
            get => Items.Transport;
            set
            {
                Items.Transport = value;
                OnPropertyChanged();
            }
        }

        public double Other
        {
            get => Items.Other;
            set
            {
                Items.Other = value;
                OnPropertyChanged();
            }
        }

        public double Income
        {
            get => Items.Income;
            set
            {
                Items.Income = value;
                OnPropertyChanged();
            }
        }

        public double Total
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
            fileJSON = fileJSON.Substring(0, index+1);

            File.WriteAllText(path, fileJSON);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            _conf.WriteJSON(Items);
        }
    }
}
