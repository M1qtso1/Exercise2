using SampleHierarchies.Data;
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
        private readonly ScreenDefinitionService _screenDefinitionService;
        private ISettingsService _settingsService;
        private ISettings _settings;
        private IDataService _dataService;
        private AnimalsScreen _animalsScreen;

        public DogsScreen(IDataService dataService, SettingsService settingsService, IScreenDefinitionService screenDefinitionService /*TestScreen testScreen*/) : base(screenDefinitionService, "DogsScreen.json")
        {
            _dataService = dataService;
            _settingsService = settingsService;
            _settings = settingsService.GetSettings();
            //ScreenDefinitionJson = "main_screen_definition.json";
            //_testScreen = testScreen;
        }
        public override void Show(  )
        {
            Console.Clear();
            // Load the screen definition from the JSON file
            var screenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson);
            int selectedIndex = 1; // Track the currently selected line
            int totalLines = screenDefinition.LineEntries.Count;
            int startIndex = 0;
            int endIndex = 5;

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
                                // Action for the first line
                                ListDogs();
                                break;
                            case 2:
                                // Action for the second line
                                AddDog();
                                break;
                            case 3:
                                // Action for the third line
                                DeleteDog();
                                break;
                            case 4:
                                // Action for the fourth line
                                EditDogMain();
                                break;
                            case 5:
                                // Action for the fifth line
                                var screenDefinitions = ScreenDefinitionService.Load(ScreenDefinitionJson);
                                var exitDogs = screenDefinitions.LineEntries[10];//Going back to parent menu.
                                Console.BackgroundColor = exitDogs.BackgroundColor;
                                Console.ForegroundColor = exitDogs.ForegroundColor;
                                Console.WriteLine(exitDogs.Text);
                                Console.ResetColor();
                                Console.Clear();
                                return;
                            default:
                                // Handle other lines or invalid selections
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
            var screenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson);
            Console.WriteLine();
            if (_dataService?.Animals?.Mammals?.Dogs is not null &&
                _dataService.Animals.Mammals.Dogs.Count > 0)
            {
                var listDogs = screenDefinition.LineEntries[6];//Here's the list of dogs:
                Console.BackgroundColor = listDogs.BackgroundColor;
                Console.ForegroundColor = listDogs.ForegroundColor;
                Console.WriteLine(listDogs.Text);
                int i = 1;

                foreach (Dog dog in _dataService.Animals.Mammals.Dogs)
                {
                    var listDog = screenDefinition.LineEntries[7];//Dog
                    Console.BackgroundColor = listDog.BackgroundColor;
                    Console.ForegroundColor = listDog.ForegroundColor;
                    Console.WriteLine(listDog.Text);
                    dog.Display();
                    i++;
                }
            }
            else
            {
                var listDogs = screenDefinition.LineEntries[8];//The list of dogs is empty.
                Console.BackgroundColor = listDogs.BackgroundColor;
                Console.ForegroundColor = listDogs.ForegroundColor;
                Console.WriteLine(listDogs.Text);
            }
        }

        /// <summary>
        /// Add a dog.
        /// </summary>
        private void AddDog()
        {
            Console.Clear();
            var screenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson);
            try
            {
                Dog dog = AddEditDog();
                _dataService?.Animals?.Mammals?.Dogs?.Add(dog);
                var addDogs = screenDefinition.LineEntries[9];//Dog has been added to a list of dogs
                Console.BackgroundColor = addDogs.BackgroundColor;
                Console.ForegroundColor = addDogs.ForegroundColor;
                Console.WriteLine(addDogs.Text, dog.Name);
            }
            catch
            {
                var addDogs = screenDefinition.LineEntries[11];//Invalid input.
                Console.BackgroundColor = addDogs.BackgroundColor;
                Console.ForegroundColor = addDogs.ForegroundColor;
                Console.WriteLine(addDogs.Text);
            }
        }

        /// <summary>
        /// Deletes a dog.
        /// </summary>
        private void DeleteDog()
        {
            Console.Clear();
            var screenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson);
            try
            {
                var deleteDogs = screenDefinition.LineEntries[12];//What is the name of the dog you want to delete?
                Console.BackgroundColor = deleteDogs.BackgroundColor;
                Console.ForegroundColor = deleteDogs.ForegroundColor;
                Console.WriteLine(deleteDogs.Text);
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
                    var deleteDog = screenDefinition.LineEntries[13];//Dog has been deleted from a list of dogs
                    Console.BackgroundColor = deleteDog.BackgroundColor;
                    Console.ForegroundColor = deleteDog.ForegroundColor;
                    Console.WriteLine(deleteDog.Text, dog.Name);
                }
                else
                {
                    var deleteDog = screenDefinition.LineEntries[14];//Dog not found.
                    Console.BackgroundColor = deleteDog.BackgroundColor;
                    Console.ForegroundColor = deleteDog.ForegroundColor;
                    Console.WriteLine(deleteDog.Text);
                }
            }
            catch
            {
                var deleteDogs = screenDefinition.LineEntries[11];//Invalid input. Try again.
                Console.BackgroundColor = deleteDogs.BackgroundColor;
                Console.ForegroundColor = deleteDogs.ForegroundColor;
                Console.WriteLine(deleteDogs.Text);
            }
        }

        /// <summary>
        /// Edits an existing dog after choice made.
        /// </summary>
        private void EditDogMain()
        {
            Console.Clear();
            var screenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson);
            try
            {
                var editDogs = screenDefinition.LineEntries[15];//What is the name of the dog you want to edit?
                Console.BackgroundColor = editDogs.BackgroundColor;
                Console.ForegroundColor = editDogs.ForegroundColor;
                Console.WriteLine(editDogs.Text);
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
                    var editDog = screenDefinition.LineEntries[16]; //Dog after edit:
                    Console.BackgroundColor = editDog.BackgroundColor;
                    Console.ForegroundColor = editDog.ForegroundColor;
                    Console.WriteLine(editDog.Text);
                    dog.Display();
                }
                else
                {
                    var editDog = screenDefinition.LineEntries[14];//Dog not found.
                    Console.BackgroundColor = editDog.BackgroundColor;
                    Console.ForegroundColor = editDog.ForegroundColor;
                    Console.WriteLine(editDog.Text);
                }
            }
            catch
            {
                var editDogs = screenDefinition.LineEntries[11];//Invalid input. Try again.
                Console.BackgroundColor = editDogs.BackgroundColor;
                Console.ForegroundColor = editDogs.ForegroundColor;
                Console.WriteLine(editDogs.Text);
            }
        }

        /// <summary>
        /// Adds/edit specific dog.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        private Dog AddEditDog()
        {
            Console.Clear();
            var screenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson);
            var addEditDogs = screenDefinition.LineEntries[17];//What name of the dog?
            Console.BackgroundColor = addEditDogs.BackgroundColor;
            Console.ForegroundColor = addEditDogs.ForegroundColor;
            Console.WriteLine(addEditDogs.Text);
            string? name = Console.ReadLine();
            var addEditDog = screenDefinition.LineEntries[18];//What is the dog's age?
            Console.BackgroundColor = addEditDog.BackgroundColor;
            Console.ForegroundColor = addEditDog.ForegroundColor;
            Console.WriteLine(addEditDog.Text);
            string? ageAsString = Console.ReadLine();
            var addEditDogsBreed = screenDefinition.LineEntries[19];//What is the dog's breed?
            Console.BackgroundColor = addEditDogsBreed.BackgroundColor;
            Console.ForegroundColor = addEditDogsBreed.ForegroundColor;
            Console.WriteLine(addEditDogsBreed.Text);
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
