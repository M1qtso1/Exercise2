using SampleHierarchies.Data;
using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Data.Mammals;
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
                                ListWhales();
                                break;
                            case 2:
                                // Action for the second line
                                AddWhale();
                                break;
                            case 3:
                                // Action for the third line
                                DeleteWhale();
                                break;
                            case 4:
                                // Action for the fourth line
                                EditWhaleMain();
                                break;
                            case 5:
                                // Action for the fifth line
                                var screenDefinitions = ScreenDefinitionService.Load(ScreenDefinitionJson);
                                var exitWhales = screenDefinitions.LineEntries[10];//Going back to parent menu.
                                Console.BackgroundColor = exitWhales.BackgroundColor;
                                Console.ForegroundColor = exitWhales.ForegroundColor;
                                Console.WriteLine(exitWhales.Text);
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
    /// List all dogs.
    /// </summary>
    private void ListWhales()
    {
        Console.Clear();
        var screenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson);
        Console.WriteLine();
        if (_dataService?.Animals?.Mammals?.Whales is not null &&
            _dataService.Animals.Mammals.Whales.Count > 0)
        {
            var listWhales = screenDefinition.LineEntries[6];//Here's the list of whales:
            Console.BackgroundColor = listWhales.BackgroundColor;
            Console.ForegroundColor = listWhales.ForegroundColor;
            Console.WriteLine(listWhales.Text);
            int i = 1;
            foreach (Whale whale in _dataService.Animals.Mammals.Whales)
            {
                var listWhale = screenDefinition.LineEntries[6];//Whale
                Console.BackgroundColor = listWhale.BackgroundColor;
                Console.ForegroundColor = listWhale.ForegroundColor;
                Console.WriteLine(listWhale.Text);
                whale.Display();
                i++;
            }
        }
        else
        {
            var listWhales = screenDefinition.LineEntries[8];//The list of whales is empty.
            Console.BackgroundColor = listWhales.BackgroundColor;
            Console.ForegroundColor = listWhales.ForegroundColor;
            Console.WriteLine(listWhales.Text);
        }
    }

    /// <summary>
    /// Add a whale.
    /// </summary>
    private void AddWhale()
    {
        Console.Clear();
        var screenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson);
        try
        {
            Whale whale = AddEditWhale();
            _dataService?.Animals?.Mammals?.Whales?.Add(whale);
            var addWhales = screenDefinition.LineEntries[9];//Whale has been added to a list of whales
            Console.BackgroundColor = addWhales.BackgroundColor;
            Console.ForegroundColor = addWhales.ForegroundColor;
            Console.WriteLine(addWhales.Text, whale.Name);
        }
        catch
        {
            var addWhales = screenDefinition.LineEntries[11];//Invalid input.
            Console.BackgroundColor = addWhales.BackgroundColor;
            Console.ForegroundColor = addWhales.ForegroundColor;
            Console.WriteLine(addWhales.Text);
        }
    }
    /// <summary>
    /// Deletes a whale.
    /// </summary>
    private void DeleteWhale()
    {
        Console.Clear();
        var screenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson);
        try
        {
            var deleteWhales = screenDefinition.LineEntries[12];// What is the name of the whale you want to delete?
            Console.BackgroundColor = deleteWhales.BackgroundColor;
            Console.ForegroundColor = deleteWhales.ForegroundColor;
            Console.WriteLine(deleteWhales.Text);
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
                var deleteWhale = screenDefinition.LineEntries[13];// Whale has been deleted from a list of whale
                Console.BackgroundColor = deleteWhale.BackgroundColor;
                Console.ForegroundColor = deleteWhale.ForegroundColor;
                Console.WriteLine(deleteWhale.Text, whale.Name);
            }
            else
            {
                var deleteWhale = screenDefinition.LineEntries[14];// Whale not found.
                Console.BackgroundColor = deleteWhale.BackgroundColor;
                Console.ForegroundColor = deleteWhale.ForegroundColor;
                Console.WriteLine(deleteWhale.Text);
            }
        }
        catch
        {
            var deleteWhales = screenDefinition.LineEntries[11]; // Invalid input. Try again.
            Console.BackgroundColor = deleteWhales.BackgroundColor;
            Console.ForegroundColor = deleteWhales.ForegroundColor;
            Console.WriteLine(deleteWhales.Text);
        }
    }

    /// <summary>
    /// Edits an existing whale after choice made.
    /// </summary>
    private void EditWhaleMain()
    {
        Console.Clear();
        var screenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson);
        try
        {
            var editWhales = screenDefinition.LineEntries[15];//What is the name of the whale you want to edit?
            Console.BackgroundColor = editWhales.BackgroundColor;
            Console.ForegroundColor = editWhales.ForegroundColor;
            Console.WriteLine(editWhales.Text);
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
                var editWhale = screenDefinition.LineEntries[16]; //Whale after edit:
                Console.BackgroundColor = editWhale.BackgroundColor;
                Console.ForegroundColor = editWhale.ForegroundColor;
                Console.WriteLine(editWhale.Text);
                whale.Display();
            }
            else
            {
                var editWhale = screenDefinition.LineEntries[14]; // Whale not found.
                Console.BackgroundColor = editWhale.BackgroundColor;
                Console.ForegroundColor = editWhale.ForegroundColor;
                Console.WriteLine(editWhale.Text);
            }
        }
        catch
        {
            var editWhales = screenDefinition.LineEntries[11];// Invalid input. Try again.
            Console.BackgroundColor = editWhales.BackgroundColor;
            Console.ForegroundColor = editWhales.ForegroundColor;
            Console.WriteLine(editWhales.Text);
        }
    }

    /// <summary>
    /// Adds/edit specific whale.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    private Whale AddEditWhale()
    {
        Console.Clear();
        var screenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson);
        var addEditWhales = screenDefinition.LineEntries[17];// What name of the whale?
        Console.BackgroundColor = addEditWhales.BackgroundColor;
        Console.ForegroundColor = addEditWhales.ForegroundColor;
        Console.WriteLine(addEditWhales.Text);
        string? name = Console.ReadLine();
        var addEditWhale = screenDefinition.LineEntries[18];// What is the whale's age?
        Console.BackgroundColor = addEditWhale.BackgroundColor;
        Console.ForegroundColor = addEditWhale.ForegroundColor;
        Console.WriteLine(addEditWhale.Text);
        string? ageAsString = Console.ReadLine();
        var addEditWhalesEcholocation = screenDefinition.LineEntries[19];// This whale has echolocation? (true/false)?
        Console.BackgroundColor = addEditWhalesEcholocation.BackgroundColor;
        Console.ForegroundColor = addEditWhalesEcholocation.ForegroundColor;
        Console.WriteLine(addEditWhalesEcholocation.Text);
        string? echolocationAsString = Console.ReadLine();
        var addEditWhalesTeeth = screenDefinition.LineEntries[20];// This whale is toothed? (true/false)
        Console.BackgroundColor = addEditWhalesTeeth.BackgroundColor;
        Console.ForegroundColor = addEditWhalesTeeth.ForegroundColor;
        Console.WriteLine(addEditWhalesTeeth.Text);
        string? toothedAsString = Console.ReadLine();
        var addEditWhalesLifespan = screenDefinition.LineEntries[21];// How long is its lifespan? (Enter a number)
        Console.BackgroundColor = addEditWhalesLifespan.BackgroundColor;
        Console.ForegroundColor = addEditWhalesLifespan.ForegroundColor;
        Console.WriteLine(addEditWhalesLifespan.Text);
        string? lifespanAsString = Console.ReadLine();
        var addEditWhalesBehavior = screenDefinition.LineEntries[22];// This whale has sociable behavior? (true/false)
        Console.BackgroundColor = addEditWhalesBehavior.BackgroundColor;
        Console.ForegroundColor = addEditWhalesBehavior.ForegroundColor;
        Console.WriteLine(addEditWhalesBehavior.Text);
        string? behaviorAsString = Console.ReadLine();
        var addEditWhalesFeed = screenDefinition.LineEntries[23];// Feeds on squid?
        Console.BackgroundColor = addEditWhalesFeed.BackgroundColor;
        Console.ForegroundColor = addEditWhalesFeed.ForegroundColor;
        Console.WriteLine(addEditWhalesFeed.Text);
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