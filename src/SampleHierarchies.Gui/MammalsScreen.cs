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

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    /// <param name="orangutanScreen">Orangutans screen</param>
    /// <param name="chimpanzeeScreen">Chimpanzees screen</param>
    /// <param name="whaleScreen">Whales screen</param>
    /// Override the Display method to use the mammal screen color from the settings
    public MammalsScreen(DogsScreen dogsScreen, OrangutanScreen orangutanScreen, ChimpanzeeScreen chimpanzeeScreen, WhaleScreen whaleScreen, SettingsService settingsService, IScreenDefinitionService screenDefinitionService /*TestScreen testScreen*/) : base(screenDefinitionService, "MammalsScreen.json")
    {
        _dogsScreen = dogsScreen;
        _orangutanScreen = orangutanScreen;
        _chimpanzeeScreen = chimpanzeeScreen;
        _whaleScreen = whaleScreen;
        _settingsService = settingsService;
        _settings = settingsService.GetSettings();
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
        while (true)
        {
            var screenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson);

            // Loop through the line entries and display them
            int startIndex = 0;
            int endIndex = 6;

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

                MammalsScreenChoices choice = (MammalsScreenChoices)Int32.Parse(choiceAsString);
                switch (choice)
                {
                    case MammalsScreenChoices.Orangutan:
                        _orangutanScreen.Show();
                        break;

                    case MammalsScreenChoices.Dogs:
                        _dogsScreen.Show();
                        break;

                    case MammalsScreenChoices.Chimpanzee:
                        _chimpanzeeScreen.Show();
                        break;

                    case MammalsScreenChoices.Whale:
                        _whaleScreen.Show();
                        break;

                    case MammalsScreenChoices.Exit:
                        var exitMammals = screenDefinition.LineEntries[7];// Going back to parent menu.
                        Console.BackgroundColor = exitMammals.BackgroundColor;
                        Console.ForegroundColor = exitMammals.ForegroundColor;
                        Console.WriteLine(exitMammals.Text);
                        Console.Clear();
                        Console.ResetColor();
                        return;
                }
            }
            catch
            {
                var errorMammals = screenDefinition.LineEntries[8];//Invalid choice. Try again.
                Console.BackgroundColor = errorMammals.BackgroundColor;
                Console.ForegroundColor = errorMammals.ForegroundColor;
                Console.WriteLine(errorMammals.Text);
            }

        }

        #endregion // Public Methods
    }
}
