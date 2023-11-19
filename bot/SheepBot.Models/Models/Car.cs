using System.ComponentModel.DataAnnotations.Schema;

namespace SheepBot.Models;

[Table("[dbo].[car]")]
public class Car : ModelBase, IEquatable<Car>
{
    // Properties
    public string Name { get; set; } = default!;
    public bool IsFree { get; set; }
    public bool IsLegacy { get; set; }

    // Relationships
    public List<CarClass> Classes { get; } = new();

    // IEquatable
    public override bool Equals(object? obj)
    {
        return base.Equals(obj);
    }

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
        var hashCode = new HashCode();
        hashCode.Add(Name, StringComparer.InvariantCultureIgnoreCase);
        hashCode.Add(IsFree);
        hashCode.Add(IsLegacy);
        return hashCode.ToHashCode();
    }
}