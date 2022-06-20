using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using ScottPlot.Avalonia;

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
