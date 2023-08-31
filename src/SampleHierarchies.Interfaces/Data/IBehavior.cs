namespace SampleHierarchies.Interfaces.Data;

/// <summary>
/// Behavior actions.
/// </summary>
public interface IBehavior
{
    #region Interface Members

    /// <summary>
    /// Describes, how the animal makes a sound.
    /// </summary>
    void MakeSound();

    /// <summary>
    /// Describes, how the animal moves.
    /// </summary>
    void Move();

    /// <summary>
    /// Displays information about animal.
    /// </summary>
    void Display();

    #endregion // Interface Members
}
