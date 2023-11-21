namespace SheepBot.Models.Joins;

public sealed record SeriesEventTimeJoin : ModelBase
{
    public long SeriesId { get; init; }
    public long EventTimeId { get; init; }
    
    // IEquatable
    public bool Equals(SeriesEventTimeJoin? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return SeriesId == other.SeriesId
               && EventTimeId == other.EventTimeId;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), SeriesId, EventTimeId);
    }
}