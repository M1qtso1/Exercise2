namespace SampleHierarchies.Interfaces.Data.Mammals;

/// <summary>
/// Interface depicting an orangutan.
/// </summary>
public interface IOrangutan : IMammal
{
    #region Interface Members
    /// <summary>
    /// Properties of orangutan.
    /// </summary>
    bool? Lifestyle { get; set; }
    bool? Thumbs { get; set; }
    int? Intelligence { get; set; }
    bool? Behavior { get; set; }
    bool? ReproductiveRate { get; set; }

    #endregion // Interface Members
}
