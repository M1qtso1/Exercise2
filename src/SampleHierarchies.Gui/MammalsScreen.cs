using SampleHierarchies.Data;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;
using System.Drawing;

namespace SampleHierarchies.Gui;

/// <summary>
/// Mammals main screen.
/// </summary>
public sealed class MammalsScreen : Screen
{
    #region Properties And Ctor
    /// <summary>
    /// Animals screen.
    /// </summary>
    private readonly ScreenDefinitionService _screenDefinitionService;
    private DogsScreen _dogsScreen;
    private OrangutanScreen _orangutanScreen;
    private ChimpanzeeScreen _chimpanzeeScreen;
    private WhaleScreen _whaleScreen;
    private ISettingsService _settingsService;
    private ISettings _settings;
    //private TestingScreen _testingScreen;

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    /// <param name="orangutanScreen">Orangutans screen</param>
    /// <param name="chimpanzeeScreen">Chimpanzees screen</param>
    /// <param name="whaleScreen">Whales screen</param>
    /// Override the Display method to use the mammal screen color from the settings
    public MammalsScreen(DogsScreen dogsScreen, OrangutanScreen orangutanScreen, ChimpanzeeScreen chimpanzeeScreen, WhaleScreen whaleScreen, SettingsService settingsService, IScreenDefinitionService screenDefinitionService /*TestScreen testScreen*/ /*TestingScreen testingScreen*/) : base(screenDefinitionService, "MammalsScreen.json")
    {
        _dogsScreen = dogsScreen;
        _orangutanScreen = orangutanScreen;
        _chimpanzeeScreen = chimpanzeeScreen;
        _whaleScreen = whaleScreen;
        _settingsService = settingsService;
        _settings = settingsService.GetSettings();
        //_testingScreen = testingScreen;
        //ScreenDefinitionJson = "mammal_screen_definition.json";
    }
    //public override string? ScreenDefinitionJson
    //{
    //    get { return "mammal_screen_definition.json"; } // Override with a specific value
    //    set { base.ScreenDefinitionJson = value; }
    //}

    public Color BackColor { get; internal set; }

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
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Clear();
                    break;
                case ConsoleKey.DownArrow:
                    selectedIndex = Math.Min(5, selectedIndex + 1);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Clear();
                    break;
                case ConsoleKey.Enter:
                    switch (selectedIndex)
                    {
                        case 1:
                            // Action for the first line
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Clear();
                            _orangutanScreen.Show();
                            break;
                        case 2:
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Clear();
                            _dogsScreen.Show();
                            break;
                        case 3:
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Clear();
                            _chimpanzeeScreen.Show();
                            break;
                        case 4:
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Clear();
                            _whaleScreen.Show();
                            break;
                        case 5:
                            // Action for the fifth line
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Clear();
                            var settingsMain = screenDefinition.LineEntries[4];// Going back to the parent menu.
                            Console.BackgroundColor = settingsMain.BackgroundColor;
                            Console.ForegroundColor = settingsMain.ForegroundColor;
                            Console.WriteLine(settingsMain.Text);
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
    #endregion
}