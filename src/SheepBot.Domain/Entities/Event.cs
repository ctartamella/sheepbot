using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SheepBot.Domain.Base;

namespace SheepBot.Domain.Entities;

[Table("event")]
[Index("Name", Name = "Index_series_name", IsUnique = true)]
public partial record Event : ModelBase
{
    [Column("name")]
    [StringLength(100)]
    public string Name { get; init; } = null!;

    [Column("role_id")]
    public long RoleId { get; init; }

    [Column("discord_server")]
    [StringLength(30)]
    public string? DiscordServer { get; init; }

    [Column("iracing_url")]
    [StringLength(50)]
    public string? IracingUrl { get; init; }

    [Column("website")]
    [StringLength(50)]
    public string? Website { get; init; }

    [InverseProperty("Event")]
    public virtual ICollection<EventClass> EventClasses { get; init; } = new List<EventClass>();

    [InverseProperty("Event")]
    public virtual ICollection<Race> Races { get; init; } = new List<Race>();

    [ForeignKey("RoleId")]
    [InverseProperty("Events")]
    public virtual Role Role { get; init; } = null!;

    [InverseProperty("Event")]
    public virtual SingletonEvent? SingletonEvent { get; init; }

    [ForeignKey("EventId")]
    [InverseProperty("Events")]
    public virtual ICollection<Race> RacesNavigation { get; } = new List<Race>();
}
