namespace SheepBot.Models.Joins;

public sealed record SeriesEventLengthJoin : ModelBase
{
    public long SeriesId { get; init; }
    public long EventLengthId { get; init; }
    
    // IEquatable
    public bool Equals(SeriesEventLengthJoin? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return SeriesId == other.SeriesId
               && EventLengthId == other.EventLengthId;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), SeriesId, EventLengthId);
    }
}