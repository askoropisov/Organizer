﻿using Organizer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Organizer.Services
{
    public class ItemsService : INotifyPropertyChanged
    {

        public static ItemsService Instance { get; } = new ItemsService();
        Items _items;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ItemsService()
        {
            Start();
        }

        public void Start()
        {
            if (_items == null)
            {
                Items? items = null;
                try
                {
                    items = FileItemJSON.ReadJSON();
                }
                catch (JsonException)
                {
                }
                catch (UnauthorizedAccessException)
                { }

                if (items == null)
                {
                    items = new Items();
                    FileItemJSON.WriteJSON(items);
                }

                Items = items;


                foreach (var property in typeof(ItemsService).GetProperties())
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property.Name));
                }
            }
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
            Eat = 0;
            Home = 0;
            Transport = 0;
            Services = 0;
            Relaxation = 0;
            Other = 0;
            Total = 0;
            OnPropertyChanged();
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            FileItemJSON.WriteJSON(Items);
        }
    }
}
