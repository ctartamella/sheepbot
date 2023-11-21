using System.ComponentModel.DataAnnotations.Schema;

namespace SheepBot.Models;

[Table("[dbo].[track]")]
public sealed record Track : ModelBase
{
    // Properties
    public string Name { get; init; } = default!;
    public bool IsFree { get; init; }
    public bool IsLegacy { get; init; }

    // Relationships
    public List<Race> Races { get; } = new();
    
    // IEquatable
    public bool Equals(Track? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return string.Equals(Name, other.Name, StringComparison.InvariantCultureIgnoreCase) 
               && IsFree == other.IsFree 
               && IsLegacy == other.IsLegacy;
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(Name, StringComparer.InvariantCultureIgnoreCase);
        hashCode.Add(IsFree);
        hashCode.Add(IsLegacy);
        return hashCode.ToHashCode();
    }
}