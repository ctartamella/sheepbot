using System.ComponentModel.DataAnnotations.Schema;

namespace SheepBot.Models;

[Table("[dbo].[track]")]
public class Track : ModelBase, IEquatable<Track>
{
    // Properties
    public string Name { get; set; } = default!;
    public bool IsFree { get; set; }
    public bool IsLegacy { get; set; }

    // Relationships
    public List<Race> Races { get; set; } = new();
    
    // IEquatable
    public bool Equals(Track? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return string.Equals(Name, other.Name, StringComparison.InvariantCultureIgnoreCase) 
               && IsFree == other.IsFree 
               && IsLegacy == other.IsLegacy;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Track)obj);
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