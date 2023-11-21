using System.ComponentModel.DataAnnotations.Schema;
using SheepBot.Models.Enums;

namespace SheepBot.Models;

[Table("[dbo].[series]")]
public sealed record Series : ModelBase
{
    // Properties
    public string Name { get; init; } = default!;
    public long RoleId { get; init; }
    public SeriesType Type { get; init; }
    public string? DiscordServer { get; init; }
    public string? IracingUrl { get; init; }
    public string? Website { get; init; }

    // Relationships
    public Role? Role { get; set; }
    public List<Race> Races { get; } = new();

    // IEquatable
    public bool Equals(Series? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name == other.Name 
               && RoleId == other.RoleId 
               && Type == other.Type 
               && string.Equals(DiscordServer, other.DiscordServer, StringComparison.InvariantCultureIgnoreCase) 
               && string.Equals(IracingUrl, other.IracingUrl, StringComparison.InvariantCultureIgnoreCase) 
               && string.Equals(Website, other.Website, StringComparison.InvariantCultureIgnoreCase);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, RoleId, (int)Type, DiscordServer, IracingUrl, Website);
    }
}