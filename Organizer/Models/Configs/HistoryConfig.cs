using Organizer.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Organizer.Models.Configs
{
    public class HistoryConfig
    {
        private readonly PathsService _paths;

        public History History { get; set; } = new History();

        public HistoryConfig(PathsService paths)
        {
            _paths = paths;
            History = ReadJSON();
        }

        public void WriteJSON(History history)
        {
            string path = Path.Combine(_paths.ConfigDirectory, "history.json").ToString();

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                string config = JsonSerializer.Serialize<History>(history, new JsonSerializerOptions() { WriteIndented = true });
                fs.Write(Encoding.Default.GetBytes(config));
            }
        }


        public History ReadJSON()
        {
            string path = Path.Combine(_paths.ConfigDirectory, "history.json").ToString();

            try
            {
                string json = File.ReadAllText(path);
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
                restored.Data = DateTime.Now.ToString("yyyy-MM-dd");
                restored.Eat = 0;
                restored.Home = 0;
                restored.Other = 0;
                restored.Relaxation = 0;
                restored.Services = 0;
                restored.Transport = 0;
                restored.Total = 0;

                return restored;
            }
        }
    }
}
