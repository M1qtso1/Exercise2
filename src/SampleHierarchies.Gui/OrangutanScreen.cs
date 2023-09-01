using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Mammals main screen.
/// </summary>
public sealed class OrangutanScreen : Screen
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
    public OrangutanScreen(IDataService dataService, IScreenDefinitionService screenDefinitionService /*TestScreen testScreen*/) : base(screenDefinitionService, "OrangutanScreen.json")
    {
        _dataService = dataService;
        //ScreenDefinitionJson = "orangutan_screen_definition.json";
    }
    //public override string? ScreenDefinitionJson
    //{
    //    get { return "orangutan_screen_definition.json"; } // Override with a specific value
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

                OrangutanScreenChoices choice = (OrangutanScreenChoices)Int32.Parse(choiceAsString);
                switch (choice)
                {
                    case OrangutanScreenChoices.List:
                        ListOrangutans();
                        break;

                    case OrangutanScreenChoices.Create:
                        AddOrangutan();
                        break;

                    case OrangutanScreenChoices.Delete:
                        DeleteOrangutan();
                        break;

                    case OrangutanScreenChoices.Modify:
                        EditOrangutanMain();
                        break;

                    case OrangutanScreenChoices.Exit:
                        var exitOrangutan = screenDefinition.LineEntries[10];//Going back to parent menu.
                        Console.BackgroundColor = exitOrangutan.BackgroundColor;
                        Console.ForegroundColor = exitOrangutan.ForegroundColor;
                        Console.WriteLine(exitOrangutan.Text);
                        Console.Clear();
                        Console.ResetColor();
                        return;
                }
            }
            catch
            {
                var errorOrangutan = screenDefinition.LineEntries[11];//Invalid input. Try again.
                Console.BackgroundColor = errorOrangutan.BackgroundColor;
                Console.ForegroundColor = errorOrangutan.ForegroundColor;
                Console.WriteLine(errorOrangutan.Text);
            }
        }
    }

    #endregion // Public Methods

    #region Private Methods

    /// <summary>
    /// List all orangutans.
    /// </summary>
    private void ListOrangutans()
    {
        var screenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson);
        Console.WriteLine();
        if (_dataService?.Animals?.Mammals?.Orangutans is not null &&
            _dataService.Animals.Mammals.Orangutans.Count > 0)
        {
            var listOrangutans = screenDefinition.LineEntries[6];//Here's the list of orangutans:
            Console.BackgroundColor = listOrangutans.BackgroundColor;
            Console.ForegroundColor = listOrangutans.ForegroundColor;
            Console.WriteLine(listOrangutans.Text);
            int i = 1;
            foreach (Orangutan orangutan in _dataService.Animals.Mammals.Orangutans)
            {
                var listOrangutan = screenDefinition.LineEntries[7];//Orangutan
                Console.BackgroundColor = listOrangutan.BackgroundColor;
                Console.ForegroundColor = listOrangutan.ForegroundColor;
                Console.WriteLine(listOrangutan.Text);
                orangutan.Display();
                i++;
            }
        }
        else
        {
            var listOrangutans = screenDefinition.LineEntries[8];//The list of orangutans is empty.
            Console.BackgroundColor = listOrangutans.BackgroundColor;
            Console.ForegroundColor = listOrangutans.ForegroundColor;
            Console.WriteLine(listOrangutans.Text);
        }
    }

    /// <summary>
    /// Add an orangutan.
    /// </summary>
    private void AddOrangutan()
    {
        var screenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson);
        try
        {
            Orangutan orangutan = AddEditOrangutan();
            _dataService?.Animals?.Mammals?.Orangutans?.Add(orangutan);
            var addOrangutans = screenDefinition.LineEntries[9];//Orangutan has been added to a list of orangutans
            Console.BackgroundColor = addOrangutans.BackgroundColor;
            Console.ForegroundColor = addOrangutans.ForegroundColor;
            Console.WriteLine(addOrangutans.Text, orangutan.Name);
        }
        catch
        {
            var addOrangutans = screenDefinition.LineEntries[11];//Invalid input.
            Console.BackgroundColor = addOrangutans.BackgroundColor;
            Console.ForegroundColor = addOrangutans.ForegroundColor;
            Console.WriteLine(addOrangutans.Text);
        }
    }
    /// <summary>
    /// Deletes an orangutan.
    /// </summary>
    private void DeleteOrangutan()
    {
        var screenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson);
        try
        {
            var deleteOrangutans = screenDefinition.LineEntries[12];// What is the name of the orangutan you want to delete?
            Console.BackgroundColor = deleteOrangutans.BackgroundColor;
            Console.ForegroundColor = deleteOrangutans.ForegroundColor;
            Console.WriteLine(deleteOrangutans.Text);
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
                var deleteOrangutan = screenDefinition.LineEntries[13];// Orangutan has been deleted from a list of orangutan
                Console.BackgroundColor = deleteOrangutan.BackgroundColor;
                Console.ForegroundColor = deleteOrangutan.ForegroundColor;
                Console.WriteLine(deleteOrangutan.Text, orangutan.Name);
            }
            else
            {
                var deleteOrangutan = screenDefinition.LineEntries[14];// Orangutan not found.
                Console.BackgroundColor = deleteOrangutan.BackgroundColor;
                Console.ForegroundColor = deleteOrangutan.ForegroundColor;
                Console.WriteLine(deleteOrangutan.Text);
            }
        }
        catch
        {
            var deleteOrangutans = screenDefinition.LineEntries[11]; // Invalid input. Try again.
            Console.BackgroundColor = deleteOrangutans.BackgroundColor;
            Console.ForegroundColor = deleteOrangutans.ForegroundColor;
            Console.WriteLine(deleteOrangutans.Text);
        }
    }

    /// <summary>
    /// Edits an existing orangutan after choice made.
    /// </summary>
    private void EditOrangutanMain()
    {
        var screenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson);
        try
        {
            var editOrangutans = screenDefinition.LineEntries[15];//What is the name of the orangutan you want to edit?
            Console.BackgroundColor = editOrangutans.BackgroundColor;
            Console.ForegroundColor = editOrangutans.ForegroundColor;
            Console.WriteLine(editOrangutans.Text);
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
                var editOrangutan = screenDefinition.LineEntries[16]; //Orangutan after edit:
                Console.BackgroundColor = editOrangutan.BackgroundColor;
                Console.ForegroundColor = editOrangutan.ForegroundColor;
                Console.WriteLine(editOrangutan.Text);
                orangutan.Display();
            }
            else
            {
                var editOrangutan = screenDefinition.LineEntries[14]; // Orangutan not found.
                Console.BackgroundColor = editOrangutan.BackgroundColor;
                Console.ForegroundColor = editOrangutan.ForegroundColor;
                Console.WriteLine(editOrangutan.Text);
            }
        }
        catch
        {
            var editOrangutans = screenDefinition.LineEntries[11];// Invalid input. Try again.
            Console.BackgroundColor = editOrangutans.BackgroundColor;
            Console.ForegroundColor = editOrangutans.ForegroundColor;
            Console.WriteLine(editOrangutans.Text);
        }
    }

    /// <summary>
    /// Adds/edit specific orangutan.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    private Orangutan AddEditOrangutan()
    {
        var screenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson);
        var addEditOrangutans = screenDefinition.LineEntries[17];// What name of the orangutan?
        Console.BackgroundColor = addEditOrangutans.BackgroundColor;
        Console.ForegroundColor = addEditOrangutans.ForegroundColor;
        Console.WriteLine(addEditOrangutans.Text);
        string? name = Console.ReadLine();
        var addEditOrangutan = screenDefinition.LineEntries[18];// What is the orangutan's age?
        Console.BackgroundColor = addEditOrangutan.BackgroundColor;
        Console.ForegroundColor = addEditOrangutan.ForegroundColor;
        Console.WriteLine(addEditOrangutan.Text);
        string? ageAsString = Console.ReadLine();
        var addEditOrangutansThumbs = screenDefinition.LineEntries[19];//This orangutan has opposable thumbs? (true/false)
        Console.BackgroundColor = addEditOrangutansThumbs.BackgroundColor;
        Console.ForegroundColor = addEditOrangutansThumbs.ForegroundColor;
        Console.WriteLine(addEditOrangutansThumbs.Text);
        string? thumbsAsString = Console.ReadLine();
        var addEditOrangutansBehavior = screenDefinition.LineEntries[20];// This orangutan has solitary behavior? (true/false)
        Console.BackgroundColor = addEditOrangutansBehavior.BackgroundColor;
        Console.ForegroundColor = addEditOrangutansBehavior.ForegroundColor;
        Console.WriteLine(addEditOrangutansBehavior.Text);
        string? behaviorAsString = Console.ReadLine();
        var addEditOrangutansLifestyle = screenDefinition.LineEntries[21];// This orangutan has arboreal lifestyle? (true/false)
        Console.BackgroundColor = addEditOrangutansLifestyle.BackgroundColor;
        Console.ForegroundColor = addEditOrangutansLifestyle.ForegroundColor;
        Console.WriteLine(addEditOrangutansLifestyle.Text);
        string? lifestyleAsString = Console.ReadLine();
        var addEditOrangutansIntelligence = screenDefinition.LineEntries[22];// What is the orangutan's level of intelligence? (Enter a number)
        Console.BackgroundColor = addEditOrangutansIntelligence.BackgroundColor;
        Console.ForegroundColor = addEditOrangutansIntelligence.ForegroundColor;
        Console.WriteLine(addEditOrangutansIntelligence.Text);
        string? intelligenceAsString = Console.ReadLine();
        var addEditOrangutansFeed = screenDefinition.LineEntries[23];// This orangutan has slow reproductive rate? (true/false)
        Console.BackgroundColor = addEditOrangutansFeed.BackgroundColor;
        Console.ForegroundColor = addEditOrangutansFeed.ForegroundColor;
        Console.WriteLine(addEditOrangutansFeed.Text);
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

    #endregion // Private Methods
}
