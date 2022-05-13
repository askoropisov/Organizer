using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizer.Models
{
    public class ScoreForMonth
    {
        public string? Data { get; set; }
        public double Eat { get; set; }
        public double Transport { get; set; }
        public double Home { get; set; }
        public double Services { get; set; }
        public double Relaxation { get; set; }
        public double Other { get; set; }
        public double Total { get; set; }
    }
}
