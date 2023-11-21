
using System.ComponentModel.DataAnnotations.Schema;

namespace SheepBot.Models;

[Table("[dbo].[role]")]
public sealed record Role : ModelBase
{
    // Properties
    public long DiscordId { get; init; }
    public string RoleName { get; init; } = default!;

    // Relationships
    public List<Series> Series { get; } = new();

    public bool Equals(Role? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return DiscordId == other.DiscordId 
               && string.Equals(RoleName, other.RoleName, StringComparison.InvariantCultureIgnoreCase);
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(DiscordId, RoleName);
    }
}