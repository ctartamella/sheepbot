using System.ComponentModel.DataAnnotations.Schema;
using SheepBot.Models.Enums;

namespace SheepBot.Models;

[Table("[dbo].[series]")]
public class Series : ModelBase, IEquatable<Series>
{
    // Properties
    public string Name { get; set; } = default!;
    public long RoleId { get; set; }
    public SeriesType Type { get; set; }
    public string? DiscordServer { get; set; }
    public string? IracingUrl { get; set; }
    public string? Website { get; set; }

    // Relationships
    public Role? Role { get; set; }
    public List<Race> Races { get; set; } = new();

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

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Series)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Role, (int)Type, DiscordServer, IracingUrl, Website);
    }
}