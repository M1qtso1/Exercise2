using System;
using System.IO;
using Newtonsoft.Json;
using SampleHierarchies.Data;
using SampleHierarchies.Interfaces.Services;

namespace SampleHierarchies.Services
{
    public class ScreenDefinitionService : IScreenDefinitionService
    {
        public ScreenDefinition Load(string jsonFileName)
        {
            if (!File.Exists(jsonFileName))
            {
                return new ScreenDefinition();
            }

            string json = File.ReadAllText(jsonFileName);
            return JsonConvert.DeserializeObject<ScreenDefinition>(json);
        }

        public bool Save(ScreenDefinition screenDefinition, string jsonFileName)
        {
            try
            {
                string json = JsonConvert.SerializeObject(screenDefinition, Formatting.Indented);
                File.WriteAllText(jsonFileName, json);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving screen definition: {ex.Message}");
                return false;
            }
        }
        public Dictionary<string, string> LoadScreenDefinitions(string jsonFilePath)
        {
            // Check if the JSON file exists
            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine("Screen definition JSON file does not exist.");
                return new Dictionary<string, string>();
            }

            try
            {
                // Read the JSON content from the file
                string jsonContent = File.ReadAllText(jsonFilePath);

                // Deserialize the JSON content into a Dictionary
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading screen definitions: {ex.Message}");
                return new Dictionary<string, string>();
            }
        }
    }
}
