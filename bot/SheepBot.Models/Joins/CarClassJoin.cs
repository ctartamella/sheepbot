using System.ComponentModel.DataAnnotations.Schema;

namespace SheepBot.Models.Joins;

[Table("[dbo].[class_car]")]
public sealed record CarClassJoin : ModelBase
{
    // Properties
    public long ClassId { get; init; }
    public long CarId { get; init; }

    // Relationships
    public Class? Class { get; set; }
    public Car? Car { get; set; }
    
    public bool Equals(CarClassJoin? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return ClassId == other.ClassId && CarId == other.CarId;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(ClassId, CarId);
    }
}