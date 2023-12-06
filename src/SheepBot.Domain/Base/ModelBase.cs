using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SheepBot.Domain.Interfaces;

namespace SheepBot.Domain.Base;

public abstract record ModelBase : IModelBase
{
    [Key]
    [Column("id")]
    public long Id { get; init; }
}