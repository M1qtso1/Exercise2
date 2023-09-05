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

    private IDataService _dataService;
    private MammalsScreen _mammalsScreen;
    private readonly ScreenDefinitionService _screenDefinitionService;
    private readonly string JsonFilePath = "AnimalsScreen.json";

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService"></param>
    /// <param name="mammalsScreen"></param>
    /// <param name="settingsService"></param>
    /// <param name="screenDefinitionService"></param>
    public AnimalsScreen(
        IDataService dataService,
        MammalsScreen mammalsScreen,
        IScreenDefinitionService screenDefinitionService,
        ScreenDefinitionService screenDefinitionServices) : base(screenDefinitionService, "AnimalsScreen.json")

    {
        _dataService = dataService;
        _mammalsScreen = mammalsScreen;
        _screenDefinitionService = screenDefinitionServices;
    }
    #endregion Properties And Ctor

    #region Public Methods

    /// <inheritdoc/>
    public void Show()
    {
        Console.Clear();
        int selectedIndex = 1; // Track the currently selected line
        int startIndex = 0;
        int endIndex = 5;

        while (true)
        {
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Black;
            for (int i = startIndex; i <= endIndex; i++)
            {
                if (i == selectedIndex)
                    Console.Write("-> "); // Indicate the selected line
                else
                    Console.Write("   ");
                _screenDefinitionService.DisplayLines(JsonFilePath, i);
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
                    selectedIndex = Math.Min(4, selectedIndex + 1);
                    Console.Clear();
                    break;
                case ConsoleKey.Enter:
                    switch (selectedIndex)
                    {
                        case 1:
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Clear();
                            _mammalsScreen.Show();
                            break;
                        case 2:
                            SaveToFile();
                            break;
                        case 3:
                            ReadFromFile();
                            break;
                        case 4:
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Clear();
                            _screenDefinitionService.DisplayLines(JsonFilePath, 4);// Going back to the parent menu.
                            Console.BackgroundColor = ConsoleColor.Black;
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
    /// Save to file.
    /// </summary>
    private void SaveToFile()
    {
        var screenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson);
        try
        {
            _screenDefinitionService.DisplayLines(JsonFilePath, 7);// Save data to file:
            var fileName = Console.ReadLine();
            if (fileName is null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            _dataService.Write(fileName);
            _screenDefinitionService.DisplayLines(JsonFilePath, 8);// Data saving was successful.
            Thread.Sleep(1000);
            Console.Clear();
        }
        catch
        {
            _screenDefinitionService.DisplayLines(JsonFilePath, 9);// Data saving was not successful.
            Thread.Sleep(1000);
            Console.Clear();
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
            _screenDefinitionService.DisplayLines(JsonFilePath, 10);// Read data from file: 
            var fileName = Console.ReadLine();
            if (fileName is null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            _dataService.Write(fileName);
            _screenDefinitionService.DisplayLines(JsonFilePath, 11);// Data reading was successful.
            Console.Write(fileName);
            Thread.Sleep(1000);
            Console.Clear();
        }
        catch
        {
            _screenDefinitionService.DisplayLines(JsonFilePath, 12);// Data reading was not successful.
            Thread.Sleep(1000);
            Console.Clear();
        }
    }
    #endregion // Private Methods
}