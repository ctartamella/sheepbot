using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SheepBot.Domain.Base;

namespace SheepBot.Domain.Entities;

[Table("role")]
[Index("RoleName", Name = "Index_role_name", IsUnique = true)]
public partial record Role : ModelBase
{
    [Column("discord_id")]
    public long DiscordId { get; init; }

    [Column("role_name")]
    [StringLength(20)]
    public string? RoleName { get; init; }

    [InverseProperty("Role")]
    public virtual ICollection<Event> Events { get; init; } = new List<Event>();
}
