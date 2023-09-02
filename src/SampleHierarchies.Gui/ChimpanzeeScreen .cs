using SampleHierarchies.Data;
using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Data.Mammals;
using SampleHierarchies.Interfaces.Services;

namespace SampleHierarchies.Gui
{
    /// <summary>
    /// Mammals main screen.
    /// </summary>
    public sealed class ChimpanzeeScreen : Screen
    {
        #region Properties And Ctor

        /// <summary>
        /// Data service.
        /// </summary>
        private IDataService _dataService;
        private object age;
        private object lifestyle;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="dataService">Data service reference</param>
        public ChimpanzeeScreen(IDataService dataService, IScreenDefinitionService screenDefinitionService /*TestScreen testScreen*/) : base(screenDefinitionService, "ChimpanzeeScreen.json")
        {
            _dataService = dataService;
            //ScreenDefinitionJson = "whale_screen_definition.json";
        }
        //public override string? ScreenDefinitionJson
        //{
        //    get { return "whale_screen_definition.json"; } // Override with a specific value
        //    set { base.ScreenDefinitionJson = value; }
        //}

        #endregion Properties And Ctor

        #region Public Methods

        /// <inheritdoc/>
        public override void Show()
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
                                ListChimpanzees();
                                break;
                            case 2:
                                // Action for the second line
                                AddChimpanzee();
                                break;
                            case 3:
                                // Action for the third line
                                DeleteChimpanzee();
                                break;
                            case 4:
                                // Action for the fourth line
                                EditChimpanzeeMain();
                                break;
                            case 5:
                                // Action for the fifth line
                                var screenDefinitions = ScreenDefinitionService.Load(ScreenDefinitionJson);
                                var exitChimpanzee = screenDefinitions.LineEntries[10];//Going back to parent menu.
                                Console.BackgroundColor = exitChimpanzee.BackgroundColor;
                                Console.ForegroundColor = exitChimpanzee.ForegroundColor;
                                Console.WriteLine(exitChimpanzee.Text);
                                Console.ResetColor();
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

        #endregion // Public Methods

        #region Private Methods

        /// <summary>
        /// List all chimpanzees.
        /// </summary>
        private void ListChimpanzees()
        {
            Console.Clear ();
            var screenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson);
            Console.WriteLine();
            if (_dataService?.Animals?.Mammals?.Chimpanzees is not null &&
                _dataService.Animals.Mammals.Chimpanzees.Count > 0)
            {
                var listChimpanzees = screenDefinition.LineEntries[6];//Here's the list of chimpanzees:
                Console.BackgroundColor = listChimpanzees.BackgroundColor;
                Console.ForegroundColor = listChimpanzees.ForegroundColor;
                Console.WriteLine(listChimpanzees.Text);
                int i = 1;
                foreach (Chimpanzee chimpanzee in _dataService.Animals.Mammals.Chimpanzees)
                {
                    var listChimpanzee = screenDefinition.LineEntries[7];//Chimpanzee
                    Console.BackgroundColor = listChimpanzee.BackgroundColor;
                    Console.ForegroundColor = listChimpanzee.ForegroundColor;
                    Console.WriteLine(listChimpanzee.Text);
                    chimpanzee.Display();
                    i++;
                }
            }
            else
            {
                var listChimpanzees = screenDefinition.LineEntries[8];//The list of chimpanzees is empty.
                Console.BackgroundColor = listChimpanzees.BackgroundColor;
                Console.ForegroundColor = listChimpanzees.ForegroundColor;
                Console.WriteLine(listChimpanzees.Text);
            }
        }

        /// <summary>
        /// Add a chimpanzee.
        /// </summary>
        private void AddChimpanzee()
        {
            Console.Clear();
            var screenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson);
            try
            {
                Chimpanzee chimpanzee = AddEditChimpanzee();
                _dataService?.Animals?.Mammals?.Chimpanzees?.Add(chimpanzee);
                var addChimpanzees = screenDefinition.LineEntries[9];//Chimpanzee has been added to a list of chimpanzees
                Console.BackgroundColor = addChimpanzees.BackgroundColor;
                Console.ForegroundColor = addChimpanzees.ForegroundColor;
                Console.WriteLine(addChimpanzees.Text, chimpanzee.Name);
            }
            catch
            {
                var addChimpanzees = screenDefinition.LineEntries[11];//Invalid input.
                Console.BackgroundColor = addChimpanzees.BackgroundColor;
                Console.ForegroundColor = addChimpanzees.ForegroundColor;
                Console.WriteLine(addChimpanzees.Text);
            }
        }
        /// <summary>
        /// Deletes a chimpanzee.
        /// </summary>
        private void DeleteChimpanzee()
        {
            Console.Clear();
            var screenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson);
            try
            {
                var deleteChimpanzees = screenDefinition.LineEntries[12];// What is the name of the chimpanzee you want to delete?
                Console.BackgroundColor = deleteChimpanzees.BackgroundColor;
                Console.ForegroundColor = deleteChimpanzees.ForegroundColor;
                Console.WriteLine(deleteChimpanzees.Text);
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
                    var deleteChimpanzee = screenDefinition.LineEntries[13];// Chimpanzee has been deleted from a list of chimpanzee
                    Console.BackgroundColor = deleteChimpanzee.BackgroundColor;
                    Console.ForegroundColor = deleteChimpanzee.ForegroundColor;
                    Console.WriteLine(deleteChimpanzee.Text, chimpanzee.Name);
                }
                else
                {
                    var deleteChimpanzee = screenDefinition.LineEntries[14];// Chimpanzee not found.
                    Console.BackgroundColor = deleteChimpanzee.BackgroundColor;
                    Console.ForegroundColor = deleteChimpanzee.ForegroundColor;
                    Console.WriteLine(deleteChimpanzee.Text);
                }
            }
            catch
            {
                var deleteChimpanzees = screenDefinition.LineEntries[11]; // Invalid input. Try again.
                Console.BackgroundColor = deleteChimpanzees.BackgroundColor;
                Console.ForegroundColor = deleteChimpanzees.ForegroundColor;
                Console.WriteLine(deleteChimpanzees.Text);
            }
        }

        /// <summary>
        /// Edits an existing chimpanzee after choice made.
        /// </summary>
        private void EditChimpanzeeMain()
        {
            Console.Clear();
            var screenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson);
            try
            {
                var editChimpanzees = screenDefinition.LineEntries[15];//What is the name of the chimpanzee you want to edit?
                Console.BackgroundColor = editChimpanzees.BackgroundColor;
                Console.ForegroundColor = editChimpanzees.ForegroundColor;
                Console.WriteLine(editChimpanzees.Text);
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
                    var editChimpanzee = screenDefinition.LineEntries[16]; //Chimpanzee after edit:
                    Console.BackgroundColor = editChimpanzee.BackgroundColor;
                    Console.ForegroundColor = editChimpanzee.ForegroundColor;
                    Console.WriteLine(editChimpanzee.Text);
                    chimpanzee.Display();
                }
                else
                {
                    var editChimpanzee = screenDefinition.LineEntries[14]; // Chimpanzee not found.
                    Console.BackgroundColor = editChimpanzee.BackgroundColor;
                    Console.ForegroundColor = editChimpanzee.ForegroundColor;
                    Console.WriteLine(editChimpanzee.Text);
                }
            }
            catch
            {
                var editChimpanzees = screenDefinition.LineEntries[11];// Invalid input. Try again.
                Console.BackgroundColor = editChimpanzees.BackgroundColor;
                Console.ForegroundColor = editChimpanzees.ForegroundColor;
                Console.WriteLine(editChimpanzees.Text);
            }
        }

        /// <summary>
        /// Adds/edit specific chimpanzee.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        private Chimpanzee AddEditChimpanzee()
        {
            Console.Clear();
            var screenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson);
            var addEditChimpanzees = screenDefinition.LineEntries[17];// What name of the chimpanzee?
            Console.BackgroundColor = addEditChimpanzees.BackgroundColor;
            Console.ForegroundColor = addEditChimpanzees.ForegroundColor;
            Console.WriteLine(addEditChimpanzees.Text);
            string? name = Console.ReadLine();
            var addEditChimpanzee = screenDefinition.LineEntries[18];// What is the chimpanzee's age?
            Console.BackgroundColor = addEditChimpanzee.BackgroundColor;
            Console.ForegroundColor = addEditChimpanzee.ForegroundColor;
            Console.WriteLine(addEditChimpanzee.Text);
            string? ageAsString = Console.ReadLine();
            var addEditChimpanzeesThumbs = screenDefinition.LineEntries[19];//This chimpanzee has opposable thumbs? (true/false)
            Console.BackgroundColor = addEditChimpanzeesThumbs.BackgroundColor;
            Console.ForegroundColor = addEditChimpanzeesThumbs.ForegroundColor;
            Console.WriteLine(addEditChimpanzeesThumbs.Text);
            string? thumbsAsString = Console.ReadLine();
            var addEditChimpanzeesBehavior = screenDefinition.LineEntries[20];// Which type of behavior it has?
            Console.BackgroundColor = addEditChimpanzeesBehavior.BackgroundColor;
            Console.ForegroundColor = addEditChimpanzeesBehavior.ForegroundColor;
            Console.WriteLine(addEditChimpanzeesBehavior.Text);
            string? behaviorAsString = Console.ReadLine();
            var addEditChimpanzeesLifestyle = screenDefinition.LineEntries[21];// This chimpanzee uses tools? (true/false)
            Console.BackgroundColor = addEditChimpanzeesLifestyle.BackgroundColor;
            Console.ForegroundColor = addEditChimpanzeesLifestyle.ForegroundColor;
            Console.WriteLine(addEditChimpanzeesLifestyle.Text);
            string? lifestyleAsString = Console.ReadLine();
            var addEditChimpanzeesIntelligence = screenDefinition.LineEntries[22];// What is the chimpanzee's level of intelligence? (Enter a number)
            Console.BackgroundColor = addEditChimpanzeesIntelligence.BackgroundColor;
            Console.ForegroundColor = addEditChimpanzeesIntelligence.ForegroundColor;
            Console.WriteLine(addEditChimpanzeesIntelligence.Text);
            string? intelligenceAsString = Console.ReadLine();
            var addEditChimpanzeesFeed = screenDefinition.LineEntries[23];// Which type of flexible diet it has?
            Console.BackgroundColor = addEditChimpanzeesFeed.BackgroundColor;
            Console.ForegroundColor = addEditChimpanzeesFeed.ForegroundColor;
            Console.WriteLine(addEditChimpanzeesFeed.Text);
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
            if (behaviorAsString is null)
            {
                throw new ArgumentNullException(nameof(behaviorAsString));
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
            string? behavior = behaviorAsString;
            bool? tool = bool.Parse(lifestyleAsString);
            int? intelligence = Int32.Parse(intelligenceAsString);
            string? diet = dietRateAsString;

            Chimpanzee chimpanzee = new Chimpanzee(name, age, thumbs, behavior, tool, intelligence, diet);

            return chimpanzee;
        }

        #endregion // Private Methods
    }
}
