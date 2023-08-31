using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using SampleHierarchies.Data;

namespace SampleHierarchies.Services
{
    public class SettingsService : ISettingsService
    {
        private ISettings _settings;
        public SettingsService()
        {
            // Initialize settings (load from file or create defaults)
            _settings = Read("Settings.json") ?? new Settings
            {
            };
        }
        public ISettings GetSettings()
        {
            return _settings;
        }
        public ISettings? Read(string jsonPath)
        {
            if (!File.Exists(jsonPath))
            {
                return null;
            }
            string json = File.ReadAllText(jsonPath);
            return JsonConvert.DeserializeObject<Settings>(json);
        }
        public void Write(ISettings settings, string jsonPath)
        {
            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(jsonPath, json);
        }
    }
}