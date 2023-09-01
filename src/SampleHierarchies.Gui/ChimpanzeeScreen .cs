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
            while (true)
            {
                var screenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson);

                int startIndex = 0;
                int endIndex = 5;

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
                Console.Clear();

                // Validate choice
                try
                {
                    if (choiceAsString is null)
                    {
                        throw new ArgumentNullException(nameof(choiceAsString));
                    }

                    ChimpanzeeScreenChoices choice = (ChimpanzeeScreenChoices)Int32.Parse(choiceAsString);
                    switch (choice)
                    {
                        case ChimpanzeeScreenChoices.List:
                            ListChimpanzees();
                            break;

                        case ChimpanzeeScreenChoices.Create:
                            AddChimpanzee(); break;

                        case ChimpanzeeScreenChoices.Delete:
                            DeleteChimpanzee();
                            break;

                        case ChimpanzeeScreenChoices.Modify:
                            EditChimpanzeeMain();
                            break;

                        case ChimpanzeeScreenChoices.Exit:
                            var exitChimpanzee = screenDefinition.LineEntries[10];//Going back to parent menu.
                            Console.BackgroundColor = exitChimpanzee.BackgroundColor;
                            Console.ForegroundColor = exitChimpanzee.ForegroundColor;
                            Console.WriteLine(exitChimpanzee.Text);
                            Console.Clear();
                            Console.ResetColor();
                            return;
                    }
                }
                catch
                {
                    var errorChimpanzee = screenDefinition.LineEntries[11];//Invalid input. Try again.
                    Console.BackgroundColor = errorChimpanzee.BackgroundColor;
                    Console.ForegroundColor = errorChimpanzee.ForegroundColor;
                    Console.WriteLine(errorChimpanzee.Text);
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
