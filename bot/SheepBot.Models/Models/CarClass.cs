namespace SheepBot.Models;

public class CarClass : ModelBase, IEquatable<CarClass>
{
    public string Name { get; set; } = default!;

    public ICollection<Car> Cars { get; } = new List<Car>();

    public bool Equals(CarClass? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name == other.Name;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((CarClass)obj);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    public static bool operator ==(CarClass? left, CarClass? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(CarClass? left, CarClass? right)
    {
        return !Equals(left, right);
    }
}