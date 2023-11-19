
using System.ComponentModel.DataAnnotations.Schema;

namespace SheepBot.Models;

[Table("[dbo].[role]")]
public class Role : ModelBase, IEquatable<Role>
{
    public long DiscordId { get; set; }
    public string RoleName { get; set; } = default!;

    public IEnumerable<Series> Series { get; set; } = new List<Series>();

    public bool Equals(Role? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return DiscordId == other.DiscordId && RoleName == other.RoleName;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Role)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(DiscordId, RoleName);
    }
}