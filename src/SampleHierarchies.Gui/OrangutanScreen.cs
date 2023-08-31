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

                OrangutanScreenChoices choice = (OrangutanScreenChoices)Int32.Parse(choiceAsString);
                switch (choice)
                {
                    case OrangutanScreenChoices.List:
                        ListOrangutans();
                        break;

                    case OrangutanScreenChoices.Create:
                        AddOrangutan(); break;

                    case OrangutanScreenChoices.Delete: 
                        DeleteOrangutan();
                        break;

                    case OrangutanScreenChoices.Modify:
                        EditOrangutanMain();
                        break;

                    case OrangutanScreenChoices.Exit:
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
    private void ListOrangutans()
    {
        Console.WriteLine();
        if (_dataService?.Animals?.Mammals?.Orangutans is not null &&
            _dataService.Animals.Mammals.Orangutans.Count > 0)
        {
            Console.WriteLine("Here's a list of orangutans:");
            int i = 1;
            foreach (Orangutan orangutan in _dataService.Animals.Mammals.Orangutans)
            {
                Console.Write($"Orangutan number {i}, ");
                orangutan.Display();
                i++;
            }
        }
        else
        {
            Console.WriteLine("The list of orangutans is empty.");
        }
    }

    /// <summary>
    /// Add a orangutan.
    /// </summary>
    private void AddOrangutan()
    {
        try
        {
            Orangutan orangutan = AddEditOrangutan();
            _dataService?.Animals?.Mammals?.Orangutans?.Add(orangutan);
            Console.WriteLine("Orangutan with name: {0} has been added to a list of orangutans", orangutan.Name);
        }
        catch
        {
            Console.WriteLine("Invalid input.");
        }
    }

    /// <summary>
    /// Deletes a orangutan.
    /// </summary>
    private void DeleteOrangutan()
    {
        try
        {
            Console.Write("What is the name of the orangutan you want to delete? ");
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
                Console.WriteLine("Orangutan with name: {0} has been deleted from a list of orangutans", orangutan.Name);
            }
            else
            {
                Console.WriteLine("Orangutan not found.");
            }
        }
        catch
        {
            Console.WriteLine("Invalid input.");
        }
    }

    /// <summary>
    /// Edits an existing orangutan after choice made.
    /// </summary>
    private void EditOrangutanMain()
    {
        try
        {
            Console.Write("What is the name of the orangutan you want to edit? ");
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
                Console.Write("Orangutan after edit:");
                orangutan.Display();
            }
            else
            {
                Console.WriteLine("Orangutan not found.");
            }
        }
        catch
        {
            Console.WriteLine("Invalid input. Try again.");
        }
    }

    /// <summary>
    /// Adds/edit specific orangutan.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    private Orangutan AddEditOrangutan()
    {
        Console.Write("What name of the orangutan? ");
        string? name = Console.ReadLine();
        Console.Write("What is the orangutan's age? (Enter a number) ");
        string? ageAsString = Console.ReadLine();
        Console.Write("This orangutan has arboreal lifestyle? (true/false) ");
        string? lifestyleAsString = Console.ReadLine();
        Console.Write("This orangutan has opposable thumbs? (true/false) ");
        string? thumbsAsString = Console.ReadLine();
        Console.Write("What is the orangutan's level of intelligence? (Enter a number) ");
        string? intelligenceAsString = Console.ReadLine();
        Console.Write("This orangutan has solitary behavior? (true/false) ");
        string? behaviorAsString = Console.ReadLine();
        Console.Write("This orangutan has slow reproductive rate? (true/false) ");
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
