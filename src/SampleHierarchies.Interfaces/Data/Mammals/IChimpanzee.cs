namespace SampleHierarchies.Interfaces.Data.Mammals;

/// <summary>
/// Interface depicting an orangutan.
/// </summary>
public interface IChimpanzee : IMammal
{
    #region Interface Members
    /// <summary>
    /// Properties of orangutan.
    /// </summary>
    bool? Thumbs { get; set; }
    string? Behavior { get; set; }
    bool? Tool { get; set; }
    int? Intelligence { get; set; }
    string? Diet { get; set; }

    #endregion // Interface Members
}
