using SheepBot.Models.Interfaces;

namespace SheepBot.Models;

public abstract record ModelBase : IModelBase
{
    public long Id { get; init; }
}