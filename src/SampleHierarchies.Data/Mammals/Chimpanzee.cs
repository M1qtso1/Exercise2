using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Data.Mammals;
using System.ComponentModel;

namespace SampleHierarchies.Data.Mammals;

/// <summary>
/// Very basic chimpanzee class.
/// </summary>
public class Chimpanzee : MammalBase, IChimpanzee
{
    #region Public Methods

    /// <inheritdoc/>
    public override void Display()
    {
        Console.WriteLine($"\nxMy name is: {Name},\nMy age is: {Age},\nThumbs: {Thumbs},\nBehavior: {Behavior},\nTool use: {Tool},\nLevel of intelligence is {Intelligence},\nFlexible diet: {Diet}\n------------------\n");
    }

    /// <inheritdoc/>
    public override void Copy(IAnimal animal)
    {
        if (animal is IChimpanzee ad)
        {
            base.Copy(animal);
            Thumbs = ad.Thumbs;
            Behavior = ad.Behavior;
            Tool = ad.Tool;
            Intelligence = ad.Intelligence;
            Diet = ad.Diet;
        }
    }

    #endregion // Public Methods

    #region Ctors And Properties

    /// <inheritdoc/>
    public bool? Thumbs { get; set; }
    public string? Behavior { get; set; }
    public bool? Tool { get; set; }
    public int? Intelligence { get; set; }
    public string? Diet { get; set; }

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="name">Name</param>
    /// <param name="age">Age</param>
    /// <param name="lifestyle">Breed</param>
    public Chimpanzee(string name, int age, bool? thumbs, string? behavior, bool? tool, int? intelligence, string? diet) : base(name, age, MammalSpecies.Chimpanzee)
    {
        Thumbs = thumbs;
        Behavior = behavior;
        Tool = tool;
        Intelligence = intelligence;
        Diet = diet;
    }

    #endregion // Ctors And Properties
}
