using Organizer.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizer.Models
{
    public class PiePlot
    {
        private readonly PathsService _path;

        public double[] Values { get; set; }
        public ScottPlot.Plot Plot { get; set; }
        public string[] labels = { "C#", "JAVA", "Python", "F#", "PHP" };

        public PiePlot(PathsService paht)
        {
            _path = paht;

            Plot = new ScottPlot.Plot(600, 400);

            Values = new double[] { 778, 43, 283, 76, 184 };
            var pie = Plot.AddPie(Values);
            pie.SliceLabels = labels;
            //pie.ShowLabels = true;
            pie.ShowPercentages = true;
            Plot.Legend();

            Plot.SaveFig(_path.ConfigDirectory + "/currentPie.png");
        }

        public void UpdatePlot()
        {
            Values = new double[] { 90, 43, 213, 30, 24 };
            var pie = Plot.AddPie(Values);
            pie.SliceLabels = labels;
            pie.ShowLabels = true;
            pie.ShowPercentages = true;
            Plot.Legend();

            Plot.SaveFig(_path.ConfigDirectory + "/currentPie.png");
        }
    }
}
