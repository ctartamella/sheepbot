namespace SheepBot.Models.Joins;

public sealed record RaceSeriesJoin : ModelBase
{
    public long RaceId { get; init; }
    public long SeriesId { get; init; }
    
    // IEquatable
    public bool Equals(RaceSeriesJoin? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return RaceId == other.RaceId
               && SeriesId == other.SeriesId;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), RaceId, SeriesId);
    }
}