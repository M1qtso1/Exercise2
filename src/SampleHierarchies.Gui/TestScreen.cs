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
            while (true)
            {
                var screenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson);

                // Loop through the line entries and display them
                foreach (var lineEntry in screenDefinition.LineEntries)
                {
                    Console.BackgroundColor = lineEntry.BackgroundColor;
                    Console.ForegroundColor = lineEntry.ForegroundColor;
                    Console.WriteLine(lineEntry.Text);
                }

                // Restore default colors
                Console.ResetColor();
                string? choiceAsString = Console.ReadLine();

                // Validate choice
                try
                {
                    if (choiceAsString is null)
                    {
                        throw new ArgumentNullException(nameof(choiceAsString));
                    }

                    MainScreenChoices choice = (MainScreenChoices)Int32.Parse(choiceAsString);
                    switch (choice)
                    {
                        case MainScreenChoices.Animals:
                            _animalsScreen.Show();
                            break;

                        case MainScreenChoices.Settings:
                            Console.WriteLine("Setings are not working at this moment.");
                            break;

                        case MainScreenChoices.Exit:
                            Console.WriteLine("Goodbye.");
                            return;
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid choice. Try again.");
                }

            }
        }
    }
}