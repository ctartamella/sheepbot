namespace SheepBot.Models;

public class Track : ModelBase
{
    public string Name { get; set; } = default!;
    public bool IsFree { get; set; }
    public bool IsLegacy { get; set; }

    public IEnumerable<Race> Races { get; set; } = new List<Race>();
}