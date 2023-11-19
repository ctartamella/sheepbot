using System.ComponentModel.DataAnnotations.Schema;
using SheepBot.Models.Enums;

namespace SheepBot.Models;

[Table("[dbo].[race]")]
public class Race : ModelBase, IEquatable<Race>
{
    // Properties
    public long SeriesId { get; set; }
    public long TrackId { get; set; }
    public DateTimeOffset? PracticeTime { get; set; }
    public DateTimeOffset QualiTime { get; set; }
    public int? Length { get; set; }
    public RaceLengthUnit? Unit { get; set; }

    // Relationships
    public Series? Series { get; set; }
    public Track? Track { get; set; }
    
    // IEquatable
    public bool Equals(Race? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return SeriesId == other.SeriesId 
               && TrackId == other.TrackId 
               && Nullable.Equals(PracticeTime, other.PracticeTime) 
               && QualiTime.Equals(other.QualiTime) 
               && Length == other.Length 
               && Unit == other.Unit;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Race)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(SeriesId, TrackId, PracticeTime, QualiTime, Length, Unit);
    }
}