using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;
using System;
using System.Collections.Generic;

namespace SampleHierarchies.Gui
{
    public sealed class MainScreen : Screen
    {
        private readonly ScreenDefinitionService _screenDefinitionService;
        private ISettingsService _settingsService;
        private ISettings _settings;
        private IDataService _dataService;
        private AnimalsScreen _animalsScreen;

        public MainScreen(IDataService dataService, AnimalsScreen animalsScreen, SettingsService settingsService, IScreenDefinitionService screenDefinitionService /*TestScreen testScreen*/) : base(screenDefinitionService, "MainScreen.json")
        {
            _dataService = dataService;
            _animalsScreen = animalsScreen;
            _settingsService = settingsService;
            _settings = settingsService.GetSettings();
            //ScreenDefinitionJson = "main_screen_definition.json";
            //_testScreen = testScreen;
        }

        // Override the ScreenDefinitionJson property
        //public override string? ScreenDefinitionJson
        //{
        //    get { return "main_screen_definition.json"; }
        //    set { base.ScreenDefinitionJson = value; }
        //}

        public override void Show()
        {
            Console.Clear();
            // Load the screen definition from the JSON file
            var screenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson);
            int selectedIndex = 1; // Track the currently selected line
            int totalLines = screenDefinition.LineEntries.Count;
            int startIndex = 0;
            int endIndex = 4;

            while (true)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                // Display the screen lines
                for (int i = startIndex; i <= endIndex; i++)
                {
                    var lineEntry = screenDefinition.LineEntries[i];
                    Console.BackgroundColor = lineEntry.BackgroundColor;
                    Console.ForegroundColor = lineEntry.ForegroundColor;
                    if (i == selectedIndex)
                        Console.Write("-> "); // Indicate the selected line
                    else
                        Console.Write("   ");
                    Console.WriteLine(lineEntry.Text);
                }

                // Handle user input
                var key = Console.ReadKey(intercept: true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = Math.Max(1, selectedIndex - 1);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Clear();
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = Math.Min(3, selectedIndex + 1);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Clear();
                        break;
                    case ConsoleKey.Enter:
                        switch (selectedIndex)
                        {
                            case 1:
                                // Action for the first line
                                var exitMain = screenDefinition.LineEntries[6];// Goodbye.
                                Console.BackgroundColor = exitMain.BackgroundColor;
                                Console.ForegroundColor = exitMain.ForegroundColor;
                                Console.WriteLine(exitMain.Text);
                                Console.ResetColor();
                                Console.Clear();
                                return;
                                break;
                            case 2:
                                // Action for the second line
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.Clear();
                                _animalsScreen.Show();
                                break;
                            case 3:
                                // Action for the fifth line
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.Clear();
                                var settingsMain = screenDefinition.LineEntries[5];// Settings are not working at this moment.
                                Console.BackgroundColor = settingsMain.BackgroundColor;
                                Console.ForegroundColor = settingsMain.ForegroundColor;
                                Console.WriteLine(settingsMain.Text);
                                break;
                            default:
                                break;
                        }
                        Console.BackgroundColor = ConsoleColor.Black;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}