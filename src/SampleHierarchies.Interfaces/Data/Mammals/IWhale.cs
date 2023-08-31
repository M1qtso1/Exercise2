namespace SampleHierarchies.Interfaces.Data.Mammals;

/// <summary>
/// Interface depicting a whale.
/// </summary>
public interface IWhale : IMammal
{
    #region Interface Members
    /// <summary>
    /// Properties of whale.
    /// </summary>
    bool? Echolocation { get; set; }
    bool? Toothed { get; set; }
    int? Lifespan { get; set; }
    bool? Behavior { get; set; }
    string? Feeds { get; set; }

    #endregion // Interface Members
}
