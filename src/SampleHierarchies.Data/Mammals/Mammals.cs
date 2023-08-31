using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Data.Mammals;

namespace SampleHierarchies.Data.Mammals;

/// <summary>
/// Mammals collection.
/// </summary>
public class Mammals : IMammals
{
    #region IMammals Implementation

    /// <inheritdoc/>
    public List<IDog> Dogs { get; set; }
    public List<IOrangutan> Orangutans { get; set; }
    public List<IChimpanzee> Chimpanzees { get; set; }
    public List<IWhale> Whales { get; set; }

    #endregion // IMammals Implementation

    #region Ctors

    /// <summary>
    /// Default ctor.
    /// </summary>
    public Mammals()
    {
        Dogs = new List<IDog>(); 
        Orangutans = new List<IOrangutan>();
        Chimpanzees = new List<IChimpanzee>();
        Whales = new List<IWhale>();
    }

    #endregion // Ctors
}
