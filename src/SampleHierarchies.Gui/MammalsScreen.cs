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
    private DogsScreen _dogsScreen;
    private OrangutanScreen _orangutanScreen;
    private ChimpanzeeScreen _chimpanzeeScreen;
    private WhaleScreen _whaleScreen;
    private readonly ScreenDefinitionService _screenDefinitionService;
    private readonly string JsonFilePath = "MammalsScreen.json";

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    /// <param name="orangutanScreen">Orangutans screen</param>
    /// <param name="chimpanzeeScreen">Chimpanzees screen</param>
    /// <param name="whaleScreen">Whales screen</param>
    /// Override the Display method to use the mammal screen color from the settings
    public MammalsScreen(DogsScreen dogsScreen, OrangutanScreen orangutanScreen, ChimpanzeeScreen chimpanzeeScreen, WhaleScreen whaleScreen, IScreenDefinitionService screenDefinitionService, ScreenDefinitionService screenDefinitionServices) : base(screenDefinitionService, "MammalsScreen.json")
    {
        _dogsScreen = dogsScreen;
        _orangutanScreen = orangutanScreen;
        _chimpanzeeScreen = chimpanzeeScreen;
        _whaleScreen = whaleScreen;
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
                    selectedIndex = Math.Min(5, selectedIndex + 1);
                    Console.Clear();
                    break;
                case ConsoleKey.Enter:
                    switch (selectedIndex)
                    {
                        case 1:
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Clear();
                            _dogsScreen.Show();
                            break;
                        case 2:
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Clear();
                            _orangutanScreen.Show();
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
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Clear();
                            _screenDefinitionService.DisplayLines(JsonFilePath, 4); // Going back to the parent menu.
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