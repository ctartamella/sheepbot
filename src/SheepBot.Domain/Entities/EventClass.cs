using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SheepBot.Domain.Base;

namespace SheepBot.Domain.Entities;

[Table("event_class")]
[Index("ClassId", "EventId", Name = "Index_series_class_1")]
public partial record EventClass : ModelBase
{
    [Column("event_id")]
    public long EventId { get; init; }

    [Column("class_id")]
    public long ClassId { get; init; }

    [ForeignKey("ClassId")]
    [InverseProperty("EventClasses")]
    public virtual Class Class { get; init; } = null!;

    [ForeignKey("EventId")]
    [InverseProperty("EventClasses")]
    public virtual Event Event { get; init; } = null!;
}
