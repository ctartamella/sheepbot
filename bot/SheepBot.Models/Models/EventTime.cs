namespace SheepBot.Models;

public sealed record EventTime : ModelBase
{
    public DateTimeOffset? PracticeTime { get; init; } 
    public DateTimeOffset QualiTime { get; init; } 
    
    public bool Equals(EventTime? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return PracticeTime == other.PracticeTime 
               && QualiTime == other.QualiTime;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), PracticeTime, QualiTime);
    }
};