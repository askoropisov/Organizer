using ReactiveUI;

namespace Organizer.ViewModels.Windows
{
    public class MessageViewModel : ViewModelBase
    {
        public MessageViewModel()
        {

        }

        private string? _message;
        public string? Message
        {
            get => _message;
            set
            {
                this.RaiseAndSetIfChanged(ref _message, value);
            }
        }

        private string? _info;
        public string? Info
        {
            get => _info;
            set
            {
                this.RaiseAndSetIfChanged(ref _info, value);
            }
        }

        private string? _header;
        public string? Header
        {
            get => _header;
            set
            {
                this.RaiseAndSetIfChanged(ref _header, value);
            }
        }
    }

}
