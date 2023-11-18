using SheepBot.Models.Enums;

namespace SheepBot.Models;

public class Series : ModelBase
{
    public string Name { get; set; } = default!;
    public Role Role { get; set; } = default!;
    public SeriesType Type { get; set; }
    public string? DiscordServer { get; set; }
    public string? IracingUrl { get; set; }
    public string? Website { get; set; }

    public IEnumerable<Race> Races { get; set; } = new List<Race>();
}