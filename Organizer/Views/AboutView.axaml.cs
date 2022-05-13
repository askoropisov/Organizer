using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Organizer.Views
{
    public partial class About : UserControl
    {
        public About()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
