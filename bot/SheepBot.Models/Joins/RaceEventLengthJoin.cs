namespace SheepBot.Models.Joins;

public sealed record RaceEventLengthJoin : ModelBase
{
    public long RaceId { get; init; }
    public long EventLengthId { get; init; }
    
    // IEquatable
    public bool Equals(RaceEventLengthJoin? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return RaceId == other.RaceId
               && EventLengthId == other.EventLengthId;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), RaceId, EventLengthId);
    }
}