namespace SheepBot.Models.Joins;

public sealed record RaceClassJoin : ModelBase
{
    public long RaceId { get; init; }
    public long ClassId { get; init; }

    public bool Equals(RaceClassJoin? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return RaceId == other.RaceId
               && ClassId == other.ClassId;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), RaceId, ClassId);
    }
}