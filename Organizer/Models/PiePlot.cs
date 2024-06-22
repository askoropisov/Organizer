using Organizer.Infrastructure.Services;
using System.Drawing;

namespace Organizer.Models
{
    public class PiePlot
    {
        private readonly PathsService _path;

        public double[] Values { get; set; }
        public ScottPlot.Plot Plot { get; set; }
        public string[] labels = { "Питание", "Транспорт", "Жилье", "Услуги", "Отдых", "Другое" };

        public PiePlot(PathsService paht)
        {
            _path = paht;

            Plot = new ScottPlot.Plot(500, 400);

            Values = new double[] { 128, 43, 283, 76, 184, 50 };
            var pie = Plot.AddPie(Values);
            pie.SliceLabels = labels;
            pie.ShowPercentages = true;
            Plot.Style(dataBackground: Color.DarkSlateGray);
            Plot.Legend();

            Plot.SaveFig(_path.ConfigDirectory + "/currentPie.png");
        }

        public void UpdatePlot(double[] values)
        {
            var pie = Plot.AddPie(values);
            pie.ShowPercentages = true;
            Plot.Style(dataBackground: Color.DarkSlateGray);
            Plot.Legend();
            //Plot.Legend(true, ScottPlot.Alignment.UpperRight);

            Plot.SaveFig(_path.ConfigDirectory + "/currentPie.png");
        }
    }
}
