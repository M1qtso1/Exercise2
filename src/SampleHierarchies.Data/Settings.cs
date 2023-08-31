using SampleHierarchies.Interfaces.Data;
using System.Runtime;
using System.Text.Json;
using SampleHierarchies.Interfaces.Data;
using System.Collections.Generic;

namespace SampleHierarchies.Data
{
    public class Settings : ISettings
    {
        public Dictionary<string, ConsoleColor> ScreenColors { get; set; }
        public Settings()
        {
            ScreenColors = new Dictionary<string, ConsoleColor>
            {
                { "MainScreen", ConsoleColor.Magenta },
                { "AnimalScreen", ConsoleColor.Red },
                { "MammalsScreen", ConsoleColor.DarkBlue },
                { "DogsScreen", ConsoleColor.Yellow }
            };
        }
    }
}