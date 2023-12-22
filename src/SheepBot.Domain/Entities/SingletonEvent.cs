using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SheepBot.Domain.Base;

namespace SheepBot.Domain.Entities;

[Table("singleton_event")]
[Index("RaceId", Name = "Index_singleton_event_1", IsUnique = true)]
public partial record SingletonEvent : ModelBase
{
    [Column("event_id")]
    public long EventId { get; init; }

    [Column("race_id")]
    public long RaceId { get; init; }

    [ForeignKey("EventId")]
    [InverseProperty("SingletonEvent")]
    public virtual Event Event { get; init; } = null!;

    [ForeignKey("RaceId")]
    [InverseProperty("SingletonEvent")]
    public virtual Race Race { get; init; } = null!;
}
