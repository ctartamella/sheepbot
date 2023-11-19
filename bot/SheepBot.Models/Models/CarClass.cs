using System.ComponentModel.DataAnnotations.Schema;

namespace SheepBot.Models;

[Table("[dbo].[class]")]
public class CarClass : ModelBase, IEquatable<CarClass>
{
    // Properties
    public string Name { get; set; } = default!;

    // Relationships
    public List<Car> Cars { get; } = new();

    // IEquatable
    public bool Equals(CarClass? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return string.Equals(Name, other.Name, StringComparison.InvariantCultureIgnoreCase);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((CarClass)obj);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}