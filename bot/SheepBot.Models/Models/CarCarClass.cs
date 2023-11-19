using System.ComponentModel.DataAnnotations.Schema;

namespace SheepBot.Models;

[Table("[dbo].[class_car]")]
public class CarCarClass : ModelBase, IEquatable<CarCarClass>
{
    // Properties
    public long ClassId { get; set; }
    public long CarId { get; set; }

    // Relationships
    public CarClass? Class { get; set; }
    public Car? Car { get; set; }
    
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
        if (obj.GetType() != GetType()) return false;
        return Equals((CarCarClass)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ClassId, CarId);
    }
}