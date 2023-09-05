using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;
using System;
using System.Collections.Generic;

namespace SampleHierarchies.Gui
{
    public sealed class ChimpanzeeScreen : Screen
    {
        private IDataService _dataService;
        private readonly ScreenDefinitionService _screenDefinitionService;
        private readonly string JsonFilePath = "ChimpanzeeScreen.json";

        public ChimpanzeeScreen(IDataService dataService, IScreenDefinitionService screenDefinitionService, ScreenDefinitionService screenDefinitionServices) : base(screenDefinitionService, "ChimpanzeeScreen.json")
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
                                ListChimpanzees();
                                break;
                            case 2:
                                AddChimpanzee();
                                break;
                            case 3:
                                DeleteChimpanzee();
                                break;
                            case 4:
                                EditChimpanzeeMain();
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
        private void ListChimpanzees()
        {
            Console.Clear();
            Console.WriteLine();
            if (_dataService?.Animals?.Mammals?.Chimpanzees is not null &&
                _dataService.Animals.Mammals.Chimpanzees.Count > 0)
            {
                _screenDefinitionService.DisplayLines(JsonFilePath, 6);//Here's the list of chimpanzees:
                int i = 1;

                foreach (Chimpanzee chimpanzee in _dataService.Animals.Mammals.Chimpanzees)
                {
                    _screenDefinitionService.DisplayLines(JsonFilePath, 7);//Chimpanzee
                    chimpanzee.Display();
                    i++;
                }
            }
            else
            {
                _screenDefinitionService.DisplayLines(JsonFilePath, 8);//The list of chimpanzees is empty.
            }
        }

        private void AddChimpanzee()
        {
            Console.Clear();
            try
            {
                Chimpanzee chimpanzee = AddEditChimpanzee();
                _dataService?.Animals?.Mammals?.Chimpanzees?.Add(chimpanzee);
                _screenDefinitionService.DisplayLines(JsonFilePath, 9); //Chimpanzee has been added to a list of chimpanzees
                Console.Write(chimpanzee.Name);
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

        private void DeleteChimpanzee()
        {
            Console.Clear();
            try
            {
                _screenDefinitionService.DisplayLines(JsonFilePath, 12); //What is the name of the chimpanzee you want to delete?
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                Chimpanzee? chimpanzee = (Chimpanzee?)(_dataService?.Animals?.Mammals?.Chimpanzees
                    ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
                if (chimpanzee is not null)
                {
                    _dataService?.Animals?.Mammals?.Chimpanzees?.Remove(chimpanzee);
                    _screenDefinitionService.DisplayLines(JsonFilePath, 13);//Chimpanzee has been deleted from a list of chimpanzees
                    Console.Write(chimpanzee.Name);
                    Thread.Sleep(1000);
                    Console.Clear();
                }
                else
                {
                    _screenDefinitionService.DisplayLines(JsonFilePath, 14);//Chimpanzee not found.
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

        private void EditChimpanzeeMain()
        {
            Console.Clear();
            try
            {
                _screenDefinitionService.DisplayLines(JsonFilePath, 15);//What is the name of the chimpanzee you want to edit?
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                Chimpanzee? chimpanzee = (Chimpanzee?)(_dataService?.Animals?.Mammals?.Chimpanzees
                    ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
                if (chimpanzee is not null)
                {
                    Chimpanzee chimpanzeeEdited = AddEditChimpanzee();
                    chimpanzee.Copy(chimpanzeeEdited);
                    _screenDefinitionService.DisplayLines(JsonFilePath, 16); //Chimpanzee after edit:
                    chimpanzee.Display();
                    Thread.Sleep(1000);
                    Console.Clear();
                }
                else
                {
                    _screenDefinitionService.DisplayLines(JsonFilePath, 14);//Chimpanzee not found.
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

        private Chimpanzee AddEditChimpanzee()
        {
            Console.Clear();
            _screenDefinitionService.DisplayLines(JsonFilePath, 17);//What name of the chimpanzee?
            string? name = Console.ReadLine();
            _screenDefinitionService.DisplayLines(JsonFilePath, 18);//What is the chimpanzee's age?
            string? ageAsString = Console.ReadLine();
            _screenDefinitionService.DisplayLines(JsonFilePath, 19);//Does the chimpanzee have opposable thumbs? (true/false)
            string? thumbsAsString = Console.ReadLine();
            _screenDefinitionService.DisplayLines(JsonFilePath, 20);//What type of behavior does the chimpanzee exhibit?
            string? behavior = Console.ReadLine();
            _screenDefinitionService.DisplayLines(JsonFilePath, 21);//Does the chimpanzee use tools? (true/false)
            string? lifestyleAsString = Console.ReadLine();
            _screenDefinitionService.DisplayLines(JsonFilePath, 22);//What is the chimpanzee's level of intelligence? (Enter a number)
            string? intelligenceAsString = Console.ReadLine();
            _screenDefinitionService.DisplayLines(JsonFilePath, 23);//What type of flexible diet does the chimpanzee have?
            string? dietRateAsString = Console.ReadLine();

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (ageAsString is null)
            {
                throw new ArgumentNullException(nameof(ageAsString));
            }
            if (thumbsAsString is null)
            {
                throw new ArgumentNullException(nameof(thumbsAsString));
            }
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }
            if (lifestyleAsString is null)
            {
                throw new ArgumentNullException(nameof(lifestyleAsString));
            }
            if (intelligenceAsString is null)
            {
                throw new ArgumentNullException(nameof(intelligenceAsString));
            }
            if (dietRateAsString is null)
            {
                throw new ArgumentNullException(nameof(dietRateAsString));
            }
            int age = Int32.Parse(ageAsString);
            bool? thumbs = bool.Parse(thumbsAsString);
            bool? tool = bool.Parse(lifestyleAsString);
            int? intelligence = Int32.Parse(intelligenceAsString);

            Chimpanzee chimpanzee = new Chimpanzee(name, age, thumbs, behavior, tool, intelligence, dietRateAsString);

            return chimpanzee;
        }
    }
}
