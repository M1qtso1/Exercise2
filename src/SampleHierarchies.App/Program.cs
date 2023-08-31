// See https://aka.ms/new-console-template for more information
using FluentAssertions.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using PeanutButter.TinyEventAggregator;
using SampleHierarchies.Data;
using SampleHierarchies.Gui;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace ImageTagger.FrontEnd.WinForms;

/// <summary>
/// Main class for starting up program.
/// </summary>
internal static class Program
{
    #region Main Method

    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    /// <param name="args">Arguments</param>
    [STAThread]
    static void Main(string[] args)
    {
        //// Create a collection of ScreenLineEntry objects
        //    List<ScreenLineEntry> screenEntries = new List<ScreenLineEntry>
        //    {
        //        new ScreenLineEntry(ConsoleColor.Red, ConsoleColor.White, "Hello, World!"),
        //    };

        //    // Create an instance of ScreenEntriesData and populate it with the collection
        //    ScreenEntriesData data = new ScreenEntriesData
        //    {
        //        Entries = screenEntries
        //    };

        //    // Serialize the data to JSON
        //    string json = JsonConvert.SerializeObject(data, Formatting.Indented);

        //    // Define the file path where you want to save the JSON
        //    string filePath = "ScreenEntriesData.json";

        //    // Write the JSON to the file
        //    //File.WriteAllText(filePath, json);

        
        //    Console.WriteLine($"Screen entries data has been saved to {filePath}.");
        //string jsonData = File.ReadAllText(filePath);

        //// Deserialize the JSON data into an instance of ScreenEntriesData
        //ScreenEntriesData deserializedData = JsonConvert.DeserializeObject<ScreenEntriesData>(jsonData);

        //// Display the text on the screen
        //foreach (var entry in deserializedData.Entries)
        //{
        //    Console.ForegroundColor = entry.ForegroundColor;
        //    Console.BackgroundColor = entry.BackgroundColor;
        //    Console.WriteLine(entry.Text);
        //    Console.ResetColor();
        //}

        var host = CreateHostBuilder().Build();
        ServiceProvider = host.Services;

        var mainScreen = ServiceProvider.GetRequiredService<MainScreen>();
        mainScreen.Show();
    }

    #endregion // Main Method

    #region Properties And Methods

    /// <summary>
    /// Service provider.
    /// </summary>
    public static IServiceProvider? ServiceProvider { get; private set; } = null;


    /// <summary>
    /// Creates a host builder.
    /// </summary>
    /// <returns></returns>
    static IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) => 
            {
                services.AddSingleton<ISettingsService, SettingsService>();
                services.AddSingleton<SettingsService, SettingsService>();
                services.AddSingleton<ISettings, Settings>();
                services.AddSingleton<IEventAggregator, EventAggregator>();
                services.AddSingleton<IDataService, DataService>();
                services.AddSingleton<MainScreen, MainScreen>();
                services.AddSingleton<ScreenDefinitionService, ScreenDefinitionService>();
                //services.AddSingleton<TestScreen, TestScreen>();
                services.AddSingleton<DogsScreen, DogsScreen>();
                services.AddSingleton<OrangutanScreen, OrangutanScreen>();
                services.AddSingleton<ChimpanzeeScreen, ChimpanzeeScreen>();
                services.AddSingleton<WhaleScreen, WhaleScreen>();
                services.AddSingleton<AnimalsScreen, AnimalsScreen>();
                services.AddSingleton<MammalsScreen, MammalsScreen>();
                services.AddSingleton<IScreenDefinitionService, ScreenDefinitionService>();
            });
    }

    #endregion // Properties And Methods

}

