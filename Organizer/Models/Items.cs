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
        public int Total { get; set; }
        public int Eat { get; set; }
        public int Transport { get; set; }
        public int Home { get; set; }
        public int Services { get; set; }
        public int Relaxation { get; set; }
        public int Other { get; set; }
        public int Income { get; set; }
    }
}
