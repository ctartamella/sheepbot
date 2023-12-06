using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SheepBot.Domain.Base;
using SheepBot.Domain.Enums;

namespace SheepBot.Domain.Entities;

[Table("race")]
public partial record Race : ModelBase
{
    [Column("track_id")]
    public long TrackId { get; init; }

    [Column("event_id")]
    public long EventId { get; init; }

    [Column("length")]
    public int Length { get; init; }

    [Column("length_unit_id")]
    public int LengthUnitId { get; init; }

    [Column("practice_time")]
    public DateTimeOffset? PracticeTime { get; init; }

    [Column("quali_time")]
    public DateTimeOffset QualiTime { get; init; }

    [Column("race_type_id")]
    public int RaceTypeId { get; init; }

    [ForeignKey("EventId")]
    [InverseProperty("Races")]
    public virtual Event Event { get; init; } = null!;

    [ForeignKey("LengthUnitId")]
    [InverseProperty("Races")]
    public virtual LengthUnit LengthUnit { get; init; }

    [InverseProperty("Race")]
    public virtual SingletonEvent? SingletonEvent { get; init; }

    [ForeignKey("TrackId")]
    [InverseProperty("Races")]
    public virtual Track Track { get; init; } = null!;

    [ForeignKey("RaceId")]
    [InverseProperty("RacesNavigation")]
    public virtual ICollection<Event> Events { get; } = new List<Event>();
}
