using Newtonsoft.Json;
using SampleHierarchies.Data;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Animals main screen.
/// </summary>
public sealed class AnimalsScreen : Screen
{
    #region Properties And Ctor

    /// <summary>
    /// Data service.
    /// </summary>
    private readonly ScreenDefinitionService _screenDefinitionService;
    private IDataService _dataService;
    private ISettingsService _settingsService;
    private ISettings _settings;

    /// <summary>
    /// Animals screen.
    /// </summary>
    private MammalsScreen _mammalsScreen;

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    /// <param name="animalsScreen">Animals screen</param>
    public AnimalsScreen(
        IDataService dataService,
        MammalsScreen mammalsScreen,
        SettingsService settingsService, IScreenDefinitionService screenDefinitionService /*TestScreen testScreen*/) : base(screenDefinitionService, "AnimalsScreen.json")

    {
        _dataService = dataService;
        _mammalsScreen = mammalsScreen;
        _settingsService = settingsService;
        _settings = settingsService.GetSettings();
        //ScreenDefinitionJson = "animal_screen_definition.json";
    }
    //public override string? ScreenDefinitionJson
    //{
    //    get { return "animal_screen_definition.json"; } // Override with a specific value
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

                AnimalsScreenChoices choice = (AnimalsScreenChoices)Int32.Parse(choiceAsString);
                switch (choice)
                {
                    case AnimalsScreenChoices.Mammals:
                        _mammalsScreen.Show();
                        break;

                    case AnimalsScreenChoices.Save:
                        SaveToFile();
                        break;

                    case AnimalsScreenChoices.Read:
                        ReadFromFile();
                        break;

                    case AnimalsScreenChoices.Exit:
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
/// Save to file.
/// </summary>
private void SaveToFile()
    {
        try
        {
            Console.Write("Save data to file: ");
            var fileName = Console.ReadLine();
            if (fileName is null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            _dataService.Write(fileName);
            Console.WriteLine("Data saving to: '{0}' was successful.", fileName);
        }
        catch
        {
            Console.WriteLine("Data saving was not successful.");
        }
    }

    /// <summary>
    /// Read data from file.
    /// </summary>
    private void ReadFromFile()
    {
        try
        {
            Console.Write("Read data from file: ");
            var fileName = Console.ReadLine();
            if (fileName is null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            _dataService.Write(fileName);
            Console.WriteLine("Data reading from: '{0}' was successful.", fileName);
        }
        catch
        {
            Console.WriteLine("Data reading from was not successful.");
        }
    }
    #endregion // Private Methods
}