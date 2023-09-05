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
        private AnimalsScreen _animalsScreen;
        private readonly ScreenDefinitionService _screenDefinitionService;
        private readonly string JsonFilePath = "MainScreen.json";

        public MainScreen(ScreenDefinitionService screenDefinitionServices, IScreenDefinitionService screenDefinitionService, AnimalsScreen animalsScreen) : base(screenDefinitionService, "MainScreen.json")
        {
            _animalsScreen = animalsScreen;
            _screenDefinitionService = screenDefinitionServices;
        }
        public void Show()
        {
            Console.Clear();
            int selectedIndex = 1; // Track the currently selected line
            int startIndex = 0;
            int endIndex = 4;

            while (true)
            {
                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.Black;
                for (int i = startIndex; i <= endIndex; i++)
                {
                    if (i == selectedIndex)
                        Console.Write("-> "); // Indicate the selected line
                    else
                        Console.Write("   ");
                    _screenDefinitionService.DisplayLines(JsonFilePath, i);
                }

                // Handle user input
                var key = Console.ReadKey(intercept: true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = Math.Max(1, selectedIndex - 1);
                        Console.Clear();
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = Math.Min(3, selectedIndex + 1);
                        Console.Clear();
                        break;
                    case ConsoleKey.Enter:
                        switch (selectedIndex)
                        {
                            case 1:
                                _screenDefinitionService.DisplayLines(JsonFilePath, 6); // Goodbye
                                Console.ResetColor();
                                Console.Clear();
                                return;
                            case 2:
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.Clear();
                                _animalsScreen.Show();
                                break;
                            case 3:
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.Clear();
                                _screenDefinitionService.DisplayLines(JsonFilePath, 5); // Settings do not work at this moment
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