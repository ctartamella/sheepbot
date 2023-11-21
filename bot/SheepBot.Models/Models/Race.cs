using System.ComponentModel.DataAnnotations.Schema;
using SheepBot.Models.Enums;

namespace SheepBot.Models;

[Table("[dbo].[race]")]
public sealed record Race : ModelBase
{
    // Properties
    public long TrackId { get; init; }

    // Relationships
    public Series? Series { get; set; }
    public Track? Track { get; set; }
    
    // IEquatable
    public bool Equals(Race? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return TrackId == other.TrackId;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(TrackId);
    }
}