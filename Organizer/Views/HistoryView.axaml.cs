using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Organizer.Views
{
    public partial class HistoryView : UserControl
    {
        public HistoryView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
