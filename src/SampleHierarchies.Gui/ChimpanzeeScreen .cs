using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Services;

namespace SampleHierarchies.Gui;

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
    public ChimpanzeeScreen(IDataService dataService, IScreenDefinitionService screenDefinitionService) : base(screenDefinitionService, "ChimpanzeeScreen.json")
    { 
        _dataService = dataService;
        //ScreenDefinitionJson = "chimpanzee_screen_definition.json";
    }
    //public override string? ScreenDefinitionJson
    //{
    //    get { return "chimpanzee_screen_definition.json"; } // Override with a specific value
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
                        Console.WriteLine("Going back to parent menu.");
                        Console.Clear();
                        return;
                }
            }
            catch
            {
                Console.WriteLine("Invalid choice. Try again.");
            }
        }
    }

    #endregion // Public Methods

    #region Private Methods

    /// <summary>
    /// List all dogs.
    /// </summary>
    private void ListChimpanzees()
    {
        Console.WriteLine();
        if (_dataService?.Animals?.Mammals?.Chimpanzees is not null &&
            _dataService.Animals.Mammals.Chimpanzees.Count > 0)
        {
            Console.WriteLine("Here's a list of chimpanzees:");
            int i = 1;
            foreach (Chimpanzee chimpanzee in _dataService.Animals.Mammals.Chimpanzees)
            {
                Console.Write($"Chimpanzee number {i}, ");
                chimpanzee.Display();
                i++;
            }
        }
        else
        {
            Console.WriteLine("The list of chimpanzees is empty.");
        }
    }

    /// <summary>
    /// Add a chimpanzee.
    /// </summary>
    private void AddChimpanzee()
    {
        try
        {
            Chimpanzee chimpanzee = AddEditChimpanzee();
            _dataService?.Animals?.Mammals?.Chimpanzees?.Add(chimpanzee);
            Console.WriteLine("Chimpanzee with name: {0} has been added to a list of chimpanzees", chimpanzee.Name);
        }
        catch
        {
            Console.WriteLine("Invalid input.");
        }
    }
    /// <summary>
    /// Deletes a chimpanzee.
    /// </summary>
    private void DeleteChimpanzee()
    {
        try
        {
            Console.Write("What is the name of the chimpanzee you want to delete? ");
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
                Console.WriteLine("Chimpanzee with name: {0} has been deleted from a list of chimpanzees", chimpanzee.Name);
            }
            else
            {
                Console.WriteLine("Chimpanzee not found.");
            }
        }
        catch
        {
            Console.WriteLine("Invalid input.");
        }
    }

    /// <summary>
    /// Edits an existing chimpanzee after choice made.
    /// </summary>
    private void EditChimpanzeeMain()
    {
        try
        {
            Console.Write("What is the name of the chimpanzee you want to edit? ");
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
                Console.Write("Chimpanzee after edit:");
                chimpanzee.Display();
            }
            else
            {
                Console.WriteLine("Chimpanzee not found.");
            }
        }
        catch
        {
            Console.WriteLine("Invalid input. Try again.");
        }
    }

    /// <summary>
    /// Adds/edit specific chimpanzee.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    private Chimpanzee AddEditChimpanzee()
    {
        Console.Write("What name of the chimpanzee? ");
        string? name = Console.ReadLine();
        Console.Write("What is the chimpanzee's age? (Enter a number) ");
        string? ageAsString = Console.ReadLine();
        Console.Write("This chimpanzee has opposable thumbs? (true/false) ");
        string? thumbsAsString = Console.ReadLine();
        Console.Write("Which type of behavior it has? ");
        string? behaviorAsString = Console.ReadLine();
        Console.Write("This chimpanzee uses tools? (true/false) ");
        string? lifestyleAsString = Console.ReadLine();
        Console.Write("What is the chimpanzee's level of intelligence? (Enter a number) ");
        string? intelligenceAsString = Console.ReadLine();
        Console.Write("Which type of flexible diet it has? ");
        string? dietRateAsString = Console.ReadLine();

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