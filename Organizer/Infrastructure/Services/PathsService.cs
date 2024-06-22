using System;
using System.IO;

namespace Organizer.Infrastructure.Services
{
    public class PathsService
    {
        private readonly string _rootDirectory;
        private readonly string _configDirectory;
        private readonly string _logDirectory;

        public PathsService(string app = "Organizer")
        {
            _rootDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), app);
            _configDirectory = Path.Combine(_rootDirectory, "Configs");
            _logDirectory = Path.Combine(_rootDirectory, "Logs");

            Directory.CreateDirectory(_rootDirectory);
            Directory.CreateDirectory(_configDirectory);
            Directory.CreateDirectory(_logDirectory);
        }

        public string ConfigDirectory => _configDirectory;
        public string LogDirectory => _logDirectory;
        public string DbDirectory => _rootDirectory;

    }
}
