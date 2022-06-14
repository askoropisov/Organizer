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
    }


    public class FileItemJSON
    {
        static public void WriteJSON(Items items)
        {
            string RoadAppDate = @"C:\Users\";
            string userName = Environment.UserName;
            RoadAppDate += userName += @"\AppData\Roaming\Organizer\FinanceAssistent";

            using (FileStream fs = new FileStream(Path.Combine(RoadAppDate, "items.json"), FileMode.OpenOrCreate))
            {
                string config = JsonSerializer.Serialize<Items>(items, new JsonSerializerOptions() { WriteIndented = true });
                fs.Write(Encoding.Default.GetBytes(config));
            }
        }

        static public async Task WriteJSONAsync(Items items)
        {
            string RoadAppDate = @"C:\Users\";
            string userName = Environment.UserName;
            RoadAppDate += userName += @"\AppData\Roaming\Organizer\FinanceAssistent";

            using (FileStream fs = new FileStream(Path.Combine(RoadAppDate, "items.json"), FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync<Items>(fs, items, new JsonSerializerOptions() { WriteIndented = true });
            }
        }

        static public Items ReadJSON()
        {
            string RoadAppDate = @"C:\Users\";
            string userName = Environment.UserName;
            RoadAppDate += userName += @"\AppData\Roaming\Organizer\FinanceAssistent";

            try
            {
                string json = File.ReadAllText(Path.Combine(RoadAppDate, "items.json"));
                Items? restored = JsonSerializer.Deserialize<Items>(json);

                if (restored == null)
                {
                    restored = new Items();
                }
                return restored;
            }
            catch
            {
                Items restored = new Items();
                restored.Eat = 0;
                restored.Home = 0;
                restored.Transport = 0;
                restored.Services = 0;
                restored.Relaxation = 0;
                restored.Other = 0;
                restored.Total = 0;

                return restored;
            }
        }
    }
}
