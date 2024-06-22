using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Organizer.Views
{
    public partial class FinanceCalculateView : UserControl
    {
        public FinanceCalculateView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
