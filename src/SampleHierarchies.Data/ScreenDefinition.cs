namespace SampleHierarchies.Data
{
    public class ScreenDefinition
    {
        public List<ScreenLineEntry> LineEntries { get; set; } = new List<ScreenLineEntry>();
        public ScreenDefinition()
        {
            LineEntries = new List<ScreenLineEntry>();
        }
    }
}
