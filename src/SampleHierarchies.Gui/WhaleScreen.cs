using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Mammals main screen.
/// </summary>
public sealed class WhaleScreen : Screen
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
    public WhaleScreen(IDataService dataService, IScreenDefinitionService screenDefinitionService /*TestScreen testScreen*/) : base(screenDefinitionService, "WhaleScreen.json")
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

                WhaleScreenChoices choice = (WhaleScreenChoices)Int32.Parse(choiceAsString);
                switch (choice)
                {
                    case WhaleScreenChoices.List:
                        ListWhales();
                        break;

                    case WhaleScreenChoices.Create:
                        AddWhale(); break;

                    case WhaleScreenChoices.Delete:
                        DeleteWhale();
                        break;

                    case WhaleScreenChoices.Modify:
                        EditWhaleMain();
                        break;

                    case WhaleScreenChoices.Exit:
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
    private void ListWhales()
    {
        Console.WriteLine();
        if (_dataService?.Animals?.Mammals?.Whales is not null &&
            _dataService.Animals.Mammals.Whales.Count > 0)
        {
            Console.WriteLine("Here's a list of whales:");
            int i = 1;
            foreach (Whale whale in _dataService.Animals.Mammals.Whales)
            {
                Console.Write($"Whale number {i}, ");
                whale.Display();
                i++;
            }
        }
        else
        {
            Console.WriteLine("The list of whales is empty.");
        }
    }

    /// <summary>
    /// Add a whale.
    /// </summary>
    private void AddWhale()
    {
        try
        {
            Whale whale = AddEditWhale();
            _dataService?.Animals?.Mammals?.Whales?.Add(whale);
            Console.WriteLine("Whale with name: {0} has been added to a list of whales", whale.Name);
        }
        catch
        {
            Console.WriteLine("Invalid input.");
        }
    }
    /// <summary>
    /// Deletes a whale.
    /// </summary>
    private void DeleteWhale()
    {
        try
        {
            Console.Write("What is the name of the whale you want to delete? ");
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
                Console.WriteLine("Whale with name: {0} has been deleted from a list of whales", whale.Name);
            }
            else
            {
                Console.WriteLine("Whale not found.");
            }
        }
        catch
        {
            Console.WriteLine("Invalid input.");
        }
    }

    /// <summary>
    /// Edits an existing whale after choice made.
    /// </summary>
    private void EditWhaleMain()
    {
        try
        {
            Console.Write("What is the name of the whale you want to edit? ");
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
                Console.Write("Whale after edit:");
                whale.Display();
            }
            else
            {
                Console.WriteLine("Whale not found.");
            }
        }
        catch
        {
            Console.WriteLine("Invalid input. Try again.");
        }
    }

    /// <summary>
    /// Adds/edit specific whale.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    private Whale AddEditWhale()
    {
        Console.Write("What name of the whale? ");
        string? name = Console.ReadLine();
        Console.Write("What is the whale's age? (Enter a number) ");
        string? ageAsString = Console.ReadLine();
        Console.Write("This whale has echolocation? (true/false) ");
        string? echolocationAsString = Console.ReadLine();
        Console.Write("This whale is toothed? (true/false) ");
        string? toothedAsString = Console.ReadLine();
        Console.Write("How long is its lifespan? (Enter a number) ");
        string? lifespanAsString = Console.ReadLine();
        Console.Write("This whale has sociable behavior? (true/false) ");
        string? behaviorAsString = Console.ReadLine();
        Console.Write("Feeds on squid? ");
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

    #endregion // Private Methods
}