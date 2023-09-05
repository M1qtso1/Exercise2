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
        public ISettings GetSettings()
        {
            return _settings;
        }
        public ISettings? Read(string jsonPath)
        {
            ISettings? result = null;
            return result;
        }
        public void Write(ISettings settings, string jsonPath)
        {
            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(jsonPath, json);
        }
    }
}