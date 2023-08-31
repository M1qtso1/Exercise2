using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Data.Mammals;
using System.ComponentModel;

namespace SampleHierarchies.Data.Mammals;

/// <summary>
/// Very basic whale class.
/// </summary>
public class Whale : MammalBase, IWhale
{
    #region Public Methods

    /// <inheritdoc/>
    public override void Display()
    {
        Console.WriteLine($"\nMy name is: {Name},\nMy age is: {Age},\nEcholocation: {Echolocation},\nToothed whale: {Toothed},\nLong lifespan: {Lifespan},\nSociable behavior: {Behavior},\nFeeds on squid: {Feeds}\n------------------\n");
    }

    /// <inheritdoc/>
    public override void Copy(IAnimal animal)
    {
        if (animal is IWhale ad)
        {
            base.Copy(animal);
            Echolocation = ad.Echolocation;
            Toothed = ad.Toothed;
            Lifespan = ad.Lifespan;
            Behavior = ad.Behavior;
            Feeds = ad.Feeds;
        }
    }

    #endregion // Public Methods

    #region Ctors And Properties

    /// <inheritdoc/>
    public bool? Echolocation { get; set; }
    public bool? Toothed { get; set; }
    public int? Lifespan { get; set; }
    public bool? Behavior { get; set; }
    public string? Feeds { get; set; }

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="name">Name</param>
    /// <param name="age">Age</param>
    /// <param name="lifestyle">Breed</param>
    public Whale(string name, int age, bool? echolocation, bool? toothed, int? lifespan, bool? behavior, string? feeds) : base(name, age, MammalSpecies.Whale)
    {
        Echolocation = echolocation;
        Toothed = toothed;
        Lifespan = lifespan;
        Behavior = behavior;
        Feeds = feeds;
    }

    #endregion // Ctors And Properties
}
