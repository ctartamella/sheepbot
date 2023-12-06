using SheepBot.Domain.Config;
using SheepBot.Domain.Interfaces;
using SheepBot.iRacing.Client.Interfaces;

namespace SheepBot.SyncWorkers.Models;

public class Config
{
    // ReSharper disable once InconsistentNaming
    public iRacingSettings iRacing { get; } = new();
}