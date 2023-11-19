using System.ComponentModel.DataAnnotations.Schema;

namespace SheepBot.Models;

[Table("[dbo].[track]")]
public class Track : ModelBase, IEquatable<Track>
{
    public string Name { get; set; } = default!;
    public bool IsFree { get; set; }
    public bool IsLegacy { get; set; }

    public IEnumerable<Race> Races { get; set; } = new List<Race>();
    
    public bool Equals(Track? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return string.Equals(Name, other.Name, StringComparison.InvariantCultureIgnoreCase) && IsFree == other.IsFree && IsLegacy == other.IsLegacy;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
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