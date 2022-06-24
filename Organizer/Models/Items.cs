using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Organizer.Models
{
    public class Items
    {
        public double Total { get; set; }
        public double Eat { get; set; }
        public double Transport { get; set; }
        public double Home { get; set; }
        public double Services { get; set; }
        public double Relaxation { get; set; }
        public double Other { get; set; }
        public double Income { get; set; }
    }
}
