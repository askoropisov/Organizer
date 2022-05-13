using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Organizer.Models
{
    public class History
    {
        public static History Instance { get; } = new History();

        public ObservableCollection<ScoreForMonth> history { get; set; } = new ObservableCollection<ScoreForMonth>();
    }


    public class FileHistoryJSON
    {
        static public void WriteJSON(History history)
        {
            string RoadAppDate = @"C:\Users\";
            string userName = Environment.UserName;
            RoadAppDate += userName += @"\AppData\Local\FinanceAssistent";

            using (FileStream fs = new FileStream(Path.Combine(RoadAppDate, "history.json"), FileMode.Create))
            {
                string config = JsonSerializer.Serialize<History>(history, new JsonSerializerOptions() { WriteIndented = true });
                fs.Write(Encoding.Default.GetBytes(config));
            }
        }

        static public async Task WriteJSONAsync(History history)
        {
            string RoadAppDate = @"C:\Users\";
            string userName = Environment.UserName;
            RoadAppDate += userName += @"\AppData\Local\FinanceAssistent";

            using (FileStream fs = new FileStream(Path.Combine(RoadAppDate, "history.json"), FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync<History>(fs, history, new JsonSerializerOptions() { WriteIndented = true });
            }
        }

        static public History ReadJSON()
        {
            string RoadAppDate = @"C:\Users\";
            string userName = Environment.UserName;
            RoadAppDate += userName += @"\AppData\Local\FinanceAssistent";

            try
            {
                string json = File.ReadAllText(Path.Combine(RoadAppDate, "history.json"));
                History? restored = JsonSerializer.Deserialize<History>(json);

                if (restored == null)
                {
                    restored = new History();
                }
                return restored;
            }
            catch
            {
                History restored = new History();
                restored.history = new ObservableCollection<ScoreForMonth>();

                return restored;
            }
        }
    }
}
