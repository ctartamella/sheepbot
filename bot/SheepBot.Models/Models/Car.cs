namespace SheepBot.Models;

public class Car : ModelBase, IEquatable<Car>
{
    // Properties
    public string Name { get; init; } = default!;
    public bool IsFree { get; init; }
    public bool IsLegacy { get; init; }

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

    public static bool operator ==(Car? left, Car? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Car? left, Car? right)
    {
        return !Equals(left, right);
    }
}