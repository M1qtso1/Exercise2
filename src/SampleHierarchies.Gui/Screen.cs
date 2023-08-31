using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Abstract base class for a screen.
/// </summary>

public abstract class Screen
{
    protected IScreenDefinitionService ScreenDefinitionService { get; }
    protected string ScreenDefinitionJson { get; }
    #region Public Methods

    /// <summary>
    /// Show the screen.
    /// </summary>  
    public Screen(IScreenDefinitionService screenDefinitionService, string screenDefinitionJson)
    {
        ScreenDefinitionService = screenDefinitionService;
        ScreenDefinitionJson = screenDefinitionJson;
    }
    public virtual void Show()
    {
        Console.WriteLine("Showing screen");
    }
    // Add a property for the screen color
    public string? ScreenColor { get; set; }

    // Other properties and methods

    // Add a method for displaying the screen in the color specified in the settings
    public virtual void Display()
    {
        // Set the background color of the console to the screen color
        Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), ScreenColor);

        // Clear the console and write some text
        Console.Clear();
        Console.WriteLine($"This is the {this.GetType().Name} screen.");
    }
    #endregion // Public Methods
}