using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;
using System;
using System.Collections.Generic;

namespace SampleHierarchies.Gui
{
    public sealed class WhaleScreen : Screen
    {
        private IDataService _dataService;
        private readonly ScreenDefinitionService _screenDefinitionService;
        private readonly string JsonFilePath = "WhaleScreen.json";

        public WhaleScreen(IScreenDefinitionService screenDefinitionService, IDataService dataService, ScreenDefinitionService screenDefinitionServices) : base(screenDefinitionService, "WhaleScreen.json")
        {
            _dataService = dataService;
            _screenDefinitionService = screenDefinitionServices;
        }
        public void Show()
        {

            Console.Clear();
            int selectedIndex = 1; // Track the currently selected line
            int startIndex = 0;
            int endIndex = 5;

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
                        selectedIndex = Math.Min(5, selectedIndex + 1);
                        Console.Clear();
                        break;
                    case ConsoleKey.Enter:
                        switch (selectedIndex)
                        {
                            case 1:
                                ListWhales();
                                break;
                            case 2:
                                AddWhale();
                                break;
                            case 3:
                                DeleteWhale();
                                break;
                            case 4:
                                EditWhaleMain();
                                break;
                            case 5:
                                _screenDefinitionService.DisplayLines(JsonFilePath, 10);
                                Thread.Sleep(500);
                                Console.Clear();
                                return;
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
        private void ListWhales()
        {
            Console.Clear();
            Console.WriteLine();
            if (_dataService?.Animals?.Mammals?.Whales is not null &&
                _dataService.Animals.Mammals.Whales.Count > 0)
            {
                _screenDefinitionService.DisplayLines(JsonFilePath, 6);//Here's the list of whales:
                int i = 1;

                foreach (Whale whale in _dataService.Animals.Mammals.Whales)
                {
                    _screenDefinitionService.DisplayLines(JsonFilePath, 7);//Whale
                    whale.Display();
                    i++;
                }
            }
            else
            {
                _screenDefinitionService.DisplayLines(JsonFilePath, 8);//The list of whales is empty.
            }
        }

        private void AddWhale()
        {
            Console.Clear();
            try
            {
                Whale whale = AddEditWhale();
                _dataService?.Animals?.Mammals?.Whales?.Add(whale);
                _screenDefinitionService.DisplayLines(JsonFilePath, 9); //Whale has been added to a list of whales
                Console.Write(whale.Name);
                Thread.Sleep(1000);
                Console.Clear();
            }
            catch
            {
                _screenDefinitionService.DisplayLines(JsonFilePath, 11);//Invalid input.
                Thread.Sleep(1000);
                Console.Clear();
            }
        }

        private void DeleteWhale()
        {
            Console.Clear();
            try
            {
                _screenDefinitionService.DisplayLines(JsonFilePath, 12); //What is the name of the whale you want to delete?
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                Whale? whale = (Whale?)(_dataService?.Animals?.Mammals?.Whales
                    ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
                if (whale is not null)
                {
                    _dataService?.Animals?.Mammals?.Whales?.Remove(whale);
                    _screenDefinitionService.DisplayLines(JsonFilePath, 13);//Whale has been deleted from a list of whales
                    Console.Write(whale.Name);
                    Thread.Sleep(1000);
                    Console.Clear();
                }
                else
                {
                    _screenDefinitionService.DisplayLines(JsonFilePath, 14);//Whale not found.
                    Thread.Sleep(1000);
                    Console.Clear();
                }
            }
            catch
            {
                _screenDefinitionService.DisplayLines(JsonFilePath, 11);//Invalid input. Try again.
                Thread.Sleep(1000);
                Console.Clear();
            }
        }

        private void EditWhaleMain()
        {
            Console.Clear();
            try
            {
                _screenDefinitionService.DisplayLines(JsonFilePath, 15);//What is the name of the whale you want to edit?
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                Whale? whale = (Whale?)(_dataService?.Animals?.Mammals?.Whales
                    ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
                if (whale is not null)
                {
                    Whale whaleEdited = AddEditWhale();
                    whale.Copy(whaleEdited);
                    _screenDefinitionService.DisplayLines(JsonFilePath, 16); //Whale after edit:
                    whale.Display();
                    Thread.Sleep(1000);
                    Console.Clear();
                }
                else
                {
                    _screenDefinitionService.DisplayLines(JsonFilePath, 14);//Whale not found.
                    Thread.Sleep(1000);
                    Console.Clear();
                }
            }
            catch
            {
                _screenDefinitionService.DisplayLines(JsonFilePath, 11);//Invalid input. Try again.
                Thread.Sleep(1000);
                Console.Clear();
            }
        }

        private Whale AddEditWhale()
        {
            Console.Clear();
            _screenDefinitionService.DisplayLines(JsonFilePath, 17);// What name of the whale?
            string? name = Console.ReadLine();
            _screenDefinitionService.DisplayLines(JsonFilePath, 18);// What is the whale's age?
            string? ageAsString = Console.ReadLine();
            _screenDefinitionService.DisplayLines(JsonFilePath, 19);// This whale has echolocation? (true/false)?
            string? echolocationAsString = Console.ReadLine();
            _screenDefinitionService.DisplayLines(JsonFilePath, 20);// This whale is toothed? (true/false)
            string? toothedAsString = Console.ReadLine();
            _screenDefinitionService.DisplayLines(JsonFilePath, 21);// How long is its lifespan? (Enter a number)
            string? lifespanAsString = Console.ReadLine();
            _screenDefinitionService.DisplayLines(JsonFilePath, 22);// This whale has sociable behavior? (true/false)
            string? behaviorAsString = Console.ReadLine();
            _screenDefinitionService.DisplayLines(JsonFilePath, 23);// Feeds on squid?
            string? feedsAsString = Console.ReadLine();

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (ageAsString is null)
            {
                throw new ArgumentNullException(nameof(ageAsString));
            }
            if (echolocationAsString is null)
            {
                throw new ArgumentNullException(nameof(echolocationAsString));
            }
            if (toothedAsString is null)
            {
                throw new ArgumentNullException(nameof(toothedAsString));
            }
            if (lifespanAsString is null)
            {
                throw new ArgumentNullException(nameof(lifespanAsString));
            }
            if (behaviorAsString is null)
            {
                throw new ArgumentNullException(nameof(behaviorAsString));
            }
            if (feedsAsString is null)
            {
                throw new ArgumentNullException(nameof(feedsAsString));
            }
            int age = Int32.Parse(ageAsString);
            bool? echolocation = bool.Parse(echolocationAsString);
            bool? toothed = bool.Parse(toothedAsString);
            int? lifespan = Int32.Parse(lifespanAsString);
            bool? behavior = bool.Parse(behaviorAsString);
            string? feeds = feedsAsString;

            Whale whale = new Whale(name, age, echolocation, toothed, lifespan, behavior, feeds);

            return whale;
        }
    }
}