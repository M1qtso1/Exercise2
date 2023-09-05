using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;
using System;
using System.Collections.Generic;

namespace SampleHierarchies.Gui
{
    public sealed class DogsScreen : Screen
    {
        private IDataService _dataService;
        private readonly ScreenDefinitionService _screenDefinitionService;
        private readonly string JsonFilePath = "DogsScreen.json";

        public DogsScreen(IScreenDefinitionService screenDefinitionService, IDataService dataService, ScreenDefinitionService screenDefinitionServices) : base(screenDefinitionService, "DogsScreen.json")
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
                                ListDogs();
                                break;
                            case 2:
                                AddDog();
                                break;
                            case 3:
                                DeleteDog();
                                break;
                            case 4:
                                EditDogMain();
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
        private void ListDogs()
        {
            Console.Clear();
            Console.WriteLine();
            if (_dataService?.Animals?.Mammals?.Dogs is not null &&
                _dataService.Animals.Mammals.Dogs.Count > 0)
            {
                _screenDefinitionService.DisplayLines(JsonFilePath, 6);//Here's the list of dogs:
                int i = 1;

                foreach (Dog dog in _dataService.Animals.Mammals.Dogs)
                {
                    _screenDefinitionService.DisplayLines(JsonFilePath, 7);//Dog
                    dog.Display();
                    i++;
                }
            }
            else
            {
                _screenDefinitionService.DisplayLines(JsonFilePath, 8);//The list of dogs is empty.
            }
        }

        private void AddDog()
        {
            Console.Clear();
            try
            {
                Dog dog = AddEditDog();
                _dataService?.Animals?.Mammals?.Dogs?.Add(dog);
                _screenDefinitionService.DisplayLines(JsonFilePath, 9); //Dog has been added to a list of dogs
                Console.Write(dog.Name);
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

        private void DeleteDog()
        {
            Console.Clear();
            try
            {
                _screenDefinitionService.DisplayLines(JsonFilePath, 12); //What is the name of the dog you want to delete?
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                Dog? dog = (Dog?)(_dataService?.Animals?.Mammals?.Dogs
                    ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
                if (dog is not null)
                {
                    _dataService?.Animals?.Mammals?.Dogs?.Remove(dog);
                    _screenDefinitionService.DisplayLines(JsonFilePath, 13);//Dog has been deleted from a list of dogs
                    Console.Write(dog.Name);
                    Thread.Sleep(1000);
                    Console.Clear();
                }
                else
                {
                    _screenDefinitionService.DisplayLines(JsonFilePath, 14);//Dog not found.
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

        private void EditDogMain()
        {
            Console.Clear();
            try
            {
                _screenDefinitionService.DisplayLines(JsonFilePath, 15);//What is the name of the dog you want to edit?
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                Dog? dog = (Dog?)(_dataService?.Animals?.Mammals?.Dogs
                    ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
                if (dog is not null)
                {
                    Dog dogEdited = AddEditDog();
                    dog.Copy(dogEdited);
                    _screenDefinitionService.DisplayLines(JsonFilePath, 16); //Dog after edit:
                    dog.Display();
                    Thread.Sleep(1000);
                    Console.Clear();
                }
                else
                {
                    _screenDefinitionService.DisplayLines(JsonFilePath, 14);//Dog not found.
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

        private Dog AddEditDog()
        {
            Console.Clear();
            _screenDefinitionService.DisplayLines(JsonFilePath, 17);//What name of the dog?
            string? name = Console.ReadLine();
            _screenDefinitionService.DisplayLines(JsonFilePath, 18);//What is the dog's age?
            string? ageAsString = Console.ReadLine();
            _screenDefinitionService.DisplayLines(JsonFilePath, 19);//What is the dog's breed?
            string? breed = Console.ReadLine();

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (ageAsString is null)
            {
                throw new ArgumentNullException(nameof(ageAsString));
            }
            if (breed is null)
            {
                throw new ArgumentNullException(nameof(breed));
            }
            int age = Int32.Parse(ageAsString);
            Dog dog = new Dog(name, age, breed);
            return dog;
        }
    }
}
