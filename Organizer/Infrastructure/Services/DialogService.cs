using Avalonia.Controls;
using Organizer.Infrastructure.Factories;
using Organizer.Infrastructure.Interfaces;
using Organizer.ViewModels.Windows;
using Organizer.Views;
using Organizer.Views.Windows;
using System.Threading.Tasks;

namespace Organizer.Infrastructure.Services
{
    public class DialogService : IDesktopAppService
    {
        private readonly ViewModelFactory _viewModelFactory;
        private readonly Window _mainWindow;

        public DialogService(ViewModelFactory viewModelFactory, MainWindow mainWindow)
        {
            _viewModelFactory = viewModelFactory;
            _mainWindow = mainWindow;
        }

        public void ShowMessage(string message, string info = null, string header = "Внимание")
        {
            Message = _viewModelFactory.Create<MessageViewModel>();
            Message.Header = header;
            Message.Message = message;
            Message.Info = info;

            var window = new MessageView() { DataContext = Message };
            window.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center;
            window.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center;
            window.Show();
        }

        MessageViewModel Message { get; set; }

        //public async Task<bool> ShowConfirmationDialogAsync(string message, bool isNoVisible = true)
        //{
        //    var vm = new MessageViewModel { Message = message };

        //    var dialogWindow = new ConfirmationDialog
        //    {
        //        DataContext = vm
        //    };

        //    var button = dialogWindow.Get<Button>("NoButton");
        //    button.IsVisible = isNoVisible;

        //    return await dialogWindow.ShowDialog<bool>(_mainWindow);
        //}

        public Task StartAsync()
        {
            return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            return Task.CompletedTask;
        }
    }

}
