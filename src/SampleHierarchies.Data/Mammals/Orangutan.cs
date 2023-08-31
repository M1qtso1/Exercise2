using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Data.Mammals;

namespace SampleHierarchies.Data.Mammals;

/// <summary>
/// Very basic orangutan class.
/// </summary>
public class Orangutan : MammalBase, IOrangutan
{
    #region Public Methods

    /// <inheritdoc/>
    public override void Display()
    {
        Console.WriteLine($"My name is: {Name},\nMy age is: {Age},\nArboreal lifestyle: {Lifestyle},\nThumbs: {Thumbs},\nLevel of intelligence is {Intelligence},\nBehavior: {Behavior},\nReproductive rate: {ReproductiveRate}\n------------------\n");
    }

    /// <inheritdoc/>
    public override void Copy(IAnimal animal)
    {
        if (animal is IOrangutan ad)
        {
            base.Copy(animal);
            Lifestyle = ad.Lifestyle;
            Thumbs = ad.Thumbs;
            Intelligence = ad.Intelligence;
            Behavior = ad.Behavior;
            ReproductiveRate = ad.ReproductiveRate;
        }
    }

    #endregion // Public Methods

    #region Ctors And Properties

    /// <inheritdoc/>
    public bool? Lifestyle { get; set; }
    public bool? Thumbs { get; set; }
    public int? Intelligence { get; set; }
    public bool? Behavior { get; set; }
    public bool? ReproductiveRate { get; set; }

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="name">Name</param>
    /// <param name="age">Age</param>
    /// <param name="lifestyle">Breed</param>
    public Orangutan(string name, int age, bool? lifestyle, bool? thumbs, int? intelligence, bool? behavior, bool? reproductiveRate) : base(name, age, MammalSpecies.Orangutan)
    {
        Lifestyle = lifestyle;
        Thumbs = thumbs;
        Intelligence = intelligence;
        Behavior = behavior;
        ReproductiveRate = reproductiveRate;
    }

    #endregion // Ctors And Properties
}
