using SheepBot.Models.Enums;

namespace SheepBot.Models;

public sealed record EventLength : ModelBase
{
    // Properties
    public int Length { get; init; }
    public int LengthUnitId { get; init; }

    // Relationships
    public LengthUnit Unit => (LengthUnit)LengthUnitId;
    
    // IEquatable
    public bool Equals(EventLength? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Length == other.Length 
               && LengthUnitId == other.LengthUnitId;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Length, LengthUnitId);
    }
}