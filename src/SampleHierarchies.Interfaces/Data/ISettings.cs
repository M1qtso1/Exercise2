using System;

namespace SampleHierarchies.Interfaces.Data
{
    public interface ISettings
    {
        Dictionary<string, ConsoleColor> ScreenColors { get; set; }
    }

}
