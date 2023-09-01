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
                        var exitDogs = screenDefinition.LineEntries[6];//Going back to parent menu.
                        Console.BackgroundColor = exitDogs.BackgroundColor;
                        Console.ForegroundColor = exitDogs.ForegroundColor;
                        Console.WriteLine(exitDogs.Text);
                        Console.ResetColor();
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
        var screenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson);
        try
        {
            var saveAnimals = screenDefinition.LineEntries[7];// Save data to file:
            Console.BackgroundColor = saveAnimals.BackgroundColor;
            Console.ForegroundColor = saveAnimals.ForegroundColor;
            Console.WriteLine(saveAnimals.Text);
            var fileName = Console.ReadLine();
            if (fileName is null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            _dataService.Write(fileName);
            var saveAnimal = screenDefinition.LineEntries[8];// Data saving was successful.
            Console.BackgroundColor = saveAnimal.BackgroundColor;
            Console.ForegroundColor = saveAnimal.ForegroundColor;
            Console.WriteLine(saveAnimal.Text, fileName);
        }
        catch
        {
            var saveAnimals = screenDefinition.LineEntries[9];// Data saving was not successful.
            Console.BackgroundColor = saveAnimals.BackgroundColor;
            Console.ForegroundColor = saveAnimals.ForegroundColor;
            Console.WriteLine(saveAnimals.Text);
        }
    }

    /// <summary>
    /// Read data from file.
    /// </summary>
    private void ReadFromFile()
    {
        var screenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson);
        try
        {
            var readAnimals = screenDefinition.LineEntries[10];// Read data from file: 
            Console.BackgroundColor = readAnimals.BackgroundColor;
            Console.ForegroundColor = readAnimals.ForegroundColor;
            Console.WriteLine(readAnimals.Text);
            var fileName = Console.ReadLine();
            if (fileName is null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            _dataService.Write(fileName);
            var readAnimal = screenDefinition.LineEntries[11];// Data reading was successful.
            Console.BackgroundColor = readAnimal.BackgroundColor;
            Console.ForegroundColor = readAnimal.ForegroundColor;
            Console.WriteLine(readAnimal.Text, fileName);
        }
        catch
        {
            var readAnimals = screenDefinition.LineEntries[11];// Data reading was not successful.
            Console.BackgroundColor = readAnimals.BackgroundColor;
            Console.ForegroundColor = readAnimals.ForegroundColor;
            Console.WriteLine(readAnimals.Text);
        }
    }
    #endregion // Private Methods
}