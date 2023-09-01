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
                int startIndex = 0;
                int endIndex = 4;

                if (startIndex >= 0 && endIndex >= startIndex && endIndex < screenDefinition.LineEntries.Count)
                {
                    for (int i = startIndex; i <= endIndex; i++)
                    {
                        var lineEntry = screenDefinition.LineEntries[i];
                        Console.BackgroundColor = lineEntry.BackgroundColor;
                        Console.ForegroundColor = lineEntry.ForegroundColor;
                        Console.WriteLine(lineEntry.Text);
                    }
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
                            var settingsMain = screenDefinition.LineEntries[5];// Settings are not working at this moment.
                            Console.BackgroundColor = settingsMain.BackgroundColor;
                            Console.ForegroundColor = settingsMain.ForegroundColor;
                            Console.WriteLine(settingsMain.Text);
                            break;

                        case MainScreenChoices.Exit:
                            var exitMain= screenDefinition.LineEntries[6];// Goodbye.
                            Console.BackgroundColor = exitMain.BackgroundColor;
                            Console.ForegroundColor = exitMain.ForegroundColor;
                            Console.WriteLine(exitMain.Text);
                            Console.ResetColor();
                            Console.Clear();
                            return;
                    }
                }
                catch
                {
                    var errorMain = screenDefinition.LineEntries[7];// Invalid choice. Try again.
                    Console.BackgroundColor = errorMain.BackgroundColor;
                    Console.ForegroundColor = errorMain.ForegroundColor;
                    Console.WriteLine(errorMain.Text);
                }

            }
        }
    }
}