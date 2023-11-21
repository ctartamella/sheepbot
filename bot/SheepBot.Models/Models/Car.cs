using System.ComponentModel.DataAnnotations.Schema;

namespace SheepBot.Models;

[Table("[dbo].[car]")]
public sealed record Car : ModelBase
{
    // Properties
    public string Name { get; init; } = default!;
    public bool IsFree { get; init; }
    public bool IsLegacy { get; init; }

    // Relationships
    public List<Class> Classes { get; } = new();

    // IEquatable
    public bool Equals(Car? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return string.Equals(Name, other.Name, StringComparison.InvariantCultureIgnoreCase) 
               && IsFree == other.IsFree 
               && IsLegacy == other.IsLegacy;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(Name, IsFree, IsLegacy);
    }
}