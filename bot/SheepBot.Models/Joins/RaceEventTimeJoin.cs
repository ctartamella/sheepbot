namespace SheepBot.Models.Joins;

public sealed record RaceEventTimeJoin : ModelBase
{
    public long RaceId { get; init; }
    public long EventTimeId { get; init; }
    
    // IEquatable
    public bool Equals(RaceEventTimeJoin? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return RaceId == other.RaceId
               && EventTimeId == other.EventTimeId;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), RaceId, EventTimeId);
    }
}