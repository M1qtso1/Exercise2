using System;
namespace SampleHierarchies.Data;
public class ScreenLineEntry
{
    public ConsoleColor BackgroundColor { get; set; }
    public ConsoleColor ForegroundColor { get; set; }
    public string Text { get; set; }

    public ScreenLineEntry(ConsoleColor backgroundColor, ConsoleColor foregroundColor, string text)
    {
        BackgroundColor = backgroundColor;
        ForegroundColor = foregroundColor;
        Text = text;
    }

    public override string ToString()
    {
        return $"Background Color: {BackgroundColor}, Foreground Color: {ForegroundColor}, Text: {Text}";
    }
}