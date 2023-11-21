namespace SheepBot.Models.Joins;

public sealed record SeriesClassJoin : ModelBase
{
    public long SeriesId { get; init; }
    public long ClassId { get; init; }
    
    // IEquatable
    public bool Equals(SeriesClassJoin? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return SeriesId == other.SeriesId
               && ClassId == other.ClassId;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), SeriesId, ClassId);
    }
}