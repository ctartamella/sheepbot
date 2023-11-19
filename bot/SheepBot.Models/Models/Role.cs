
using System.ComponentModel.DataAnnotations.Schema;

namespace SheepBot.Models;

[Table("[dbo].[role]")]
public class Role : ModelBase, IEquatable<Role>
{
    // Properties
    public long DiscordId { get; set; }
    public string RoleName { get; set; } = default!;

    // Relationships
    public List<Series> Series { get; set; } = new();

    public bool Equals(Role? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return DiscordId == other.DiscordId 
               && string.Equals(RoleName, other.RoleName, StringComparison.InvariantCultureIgnoreCase);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Role)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(DiscordId, RoleName);
    }
}