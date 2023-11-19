namespace SheepBot.Models;

public class CarCarClass : ModelBase, IEquatable<CarCarClass>
{
    public int ClassId { get; set; }
    public int CarId { get; set; }

    public bool Equals(CarCarClass? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return ClassId == other.ClassId && CarId == other.CarId;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((CarCarClass)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ClassId, CarId);
    }

    public static bool operator ==(CarCarClass? left, CarCarClass? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(CarCarClass? left, CarCarClass? right)
    {
        return !Equals(left, right);
    }
}