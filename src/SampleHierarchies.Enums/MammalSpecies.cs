using System.ComponentModel;
/// <summary>
/// Dummy enum.
/// </summary>
public enum MammalSpecies
{
    [Description("Simple description of a none")]
    None = 0,
    [Description("Simple description of a dog")]
    Dog = 1,
    [Description("Simple description of an orangutan")]
    Orangutan = 2,
    [Description("Simple description of a chimpanzee")]
    Chimpanzee = 3,
    [Description("Simple description of a whale")]
    Whale = 3,
}
