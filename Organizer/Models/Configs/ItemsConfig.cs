using Organizer.Infrastructure.Services;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Organizer.Models.Configs
{
    public class ItemsConfig
    {
        private readonly PathsService _paths;
        public Items Items { get; set; } = new Items();

        public ItemsConfig(PathsService paths)
        {
            _paths = paths;
            Items = ReadJSON();
        }

        public void WriteJSON(Items items)
        {
            string path = Path.Combine(_paths.ConfigDirectory, "items.json").ToString();

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                string config = JsonSerializer.Serialize<Items>(items, new JsonSerializerOptions() { WriteIndented = true });
                fs.Write(Encoding.Default.GetBytes(config));
            }
        }

        public Items ReadJSON()
        {
            string path = Path.Combine(_paths.ConfigDirectory, "items.json").ToString();
            try
            {
                string json = File.ReadAllText(path);
                Items? restored = JsonSerializer.Deserialize<Items>(json);

                if (restored == null)
                {
                    restored = new Items();
                    restored.Eat = 0;
                    restored.Home = 0;
                    restored.Transport = 0;
                    restored.Services = 0;
                    restored.Relaxation = 0;
                    restored.Other = 0;
                    restored.Total = 0;
                    restored.Income = 0;
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
                restored.Income = 0;

                return restored;
            }
        }
    }
}
