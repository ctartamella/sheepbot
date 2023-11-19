using System.ComponentModel.DataAnnotations.Schema;
using SheepBot.Models.Enums;

namespace SheepBot.Models;

[Table("[dbo].[series]")]
public class Series : ModelBase, IEquatable<Series>
{
    public string Name { get; set; } = default!;
    public int RoleId { get; set; }
    public SeriesType Type { get; set; }
    public string? DiscordServer { get; set; }
    public string? IracingUrl { get; set; }
    public string? Website { get; set; }

    public Role Role { get; set; } = default!;
    public IEnumerable<Race> Races { get; set; } = new List<Race>();

    public bool Equals(Series? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name == other.Name 
               && RoleId == other.RoleId 
               && Type == other.Type 
               && DiscordServer == other.DiscordServer 
               && IracingUrl == other.IracingUrl 
               && Website == other.Website;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Series)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Role, (int)Type, DiscordServer, IracingUrl, Website);
    }
}