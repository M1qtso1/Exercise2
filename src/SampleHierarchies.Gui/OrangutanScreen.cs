using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;
using System;
using System.Collections.Generic;

namespace SampleHierarchies.Gui
{
    public sealed class OrangutanScreen : Screen
    {
        private IDataService _dataService;
        private readonly ScreenDefinitionService _screenDefinitionService;
        private readonly string JsonFilePath = "OrangutanScreen.json";

        public OrangutanScreen(IScreenDefinitionService screenDefinitionService, IDataService dataService, ScreenDefinitionService screenDefinitionServices) : base(screenDefinitionService, "OrangutanScreen.json")
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
                                ListOrangutans();
                                break;
                            case 2:
                                AddOrangutan();
                                break;
                            case 3:
                                DeleteOrangutan();
                                break;
                            case 4:
                                EditOrangutanMain();
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
        private void ListOrangutans()
        {
            Console.Clear();
            Console.WriteLine();
            if (_dataService?.Animals?.Mammals?.Orangutans is not null &&
                _dataService.Animals.Mammals.Orangutans.Count > 0)
            {
                _screenDefinitionService.DisplayLines(JsonFilePath, 6);//Here's the list of orangutans:
                int i = 1;

                foreach (Orangutan orangutan in _dataService.Animals.Mammals.Orangutans)
                {
                    _screenDefinitionService.DisplayLines(JsonFilePath, 7);//Orangutan
                    orangutan.Display();
                    i++;
                }
            }
            else
            {
                _screenDefinitionService.DisplayLines(JsonFilePath, 8);//The list of orangutans is empty.
            }
        }

        private void AddOrangutan()
        {
            Console.Clear();
            try
            {
                Orangutan orangutan = AddEditOrangutan();
                _dataService?.Animals?.Mammals?.Orangutans?.Add(orangutan);
                _screenDefinitionService.DisplayLines(JsonFilePath, 9); //Orangutan has been added to a list of orangutans
                Console.Write(orangutan.Name);
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

        private void DeleteOrangutan()
        {
            Console.Clear();
            try
            {
                _screenDefinitionService.DisplayLines(JsonFilePath, 12); //What is the name of the orangutan you want to delete?
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                Orangutan? orangutan = (Orangutan?)(_dataService?.Animals?.Mammals?.Orangutans
                    ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
                if (orangutan is not null)
                {
                    _dataService?.Animals?.Mammals?.Orangutans?.Remove(orangutan);
                    _screenDefinitionService.DisplayLines(JsonFilePath, 13);//Orangutan has been deleted from a list of orangutans
                    Console.Write(orangutan.Name);
                    Thread.Sleep(1000);
                    Console.Clear();
                }
                else
                {
                    _screenDefinitionService.DisplayLines(JsonFilePath, 14);//Orangutan not found.
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

        private void EditOrangutanMain()
        {
            Console.Clear();
            try
            {
                _screenDefinitionService.DisplayLines(JsonFilePath, 15);//What is the name of the orangutan you want to edit?
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                Orangutan? orangutan = (Orangutan?)(_dataService?.Animals?.Mammals?.Orangutans
                    ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
                if (orangutan is not null)
                {
                    Orangutan orangutanEdited = AddEditOrangutan();
                    orangutan.Copy(orangutanEdited);
                    _screenDefinitionService.DisplayLines(JsonFilePath, 16); //Orangutan after edit:
                    orangutan.Display();
                    Thread.Sleep(1000);
                    Console.Clear();
                }
                else
                {
                    _screenDefinitionService.DisplayLines(JsonFilePath, 14);//Orangutan not found.
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

        private Orangutan AddEditOrangutan()
        {
            Console.Clear();
            _screenDefinitionService.DisplayLines(JsonFilePath, 17);// What name of the orangutan?
            string? name = Console.ReadLine();
            _screenDefinitionService.DisplayLines(JsonFilePath, 18);// What is the orangutan's age?
            string? ageAsString = Console.ReadLine();
            _screenDefinitionService.DisplayLines(JsonFilePath, 19);// Does the orangutan have opposable thumbs? (true/false)
            string? thumbsAsString = Console.ReadLine();
            _screenDefinitionService.DisplayLines(JsonFilePath, 20);// Does the orangutan have solitary behavior? (true/false)
            string? behaviorAsString = Console.ReadLine();
            _screenDefinitionService.DisplayLines(JsonFilePath, 21);// This orangutan has arboreal lifestyle? (true/false)
            string? lifestyleAsString = Console.ReadLine();
            _screenDefinitionService.DisplayLines(JsonFilePath, 22);// What is the orangutan's level of intelligence? (Enter a number)
            string? intelligenceAsString = Console.ReadLine();
            _screenDefinitionService.DisplayLines(JsonFilePath, 23);// This orangutan has slow reproductive rate? (true/false)
            string? reproductiveRateAsString = Console.ReadLine();

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (ageAsString is null)
            {
                throw new ArgumentNullException(nameof(ageAsString));
            }
            if (lifestyleAsString is null)
            {
                throw new ArgumentNullException(nameof(lifestyleAsString));
            }
            if (thumbsAsString is null)
            {
                throw new ArgumentNullException(nameof(thumbsAsString));
            }
            if (intelligenceAsString is null)
            {
                throw new ArgumentNullException(nameof(intelligenceAsString));
            }
            if (behaviorAsString is null)
            {
                throw new ArgumentNullException(nameof(behaviorAsString));
            }
            if (reproductiveRateAsString is null)
            {
                throw new ArgumentNullException(nameof(reproductiveRateAsString));
            }
            int age = Int32.Parse(ageAsString);
            bool? lifestyle = bool.Parse(lifestyleAsString);
            bool? thumbs = bool.Parse(thumbsAsString);
            int? intelligence = Int32.Parse(intelligenceAsString);
            bool? behavior = bool.Parse(behaviorAsString);
            bool? reproductiveRate = bool.Parse(reproductiveRateAsString);

            Orangutan orangutan = new Orangutan(name, age, lifestyle, thumbs, intelligence, behavior, reproductiveRate);

            return orangutan;
        }
    }
}