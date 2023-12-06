using SheepBot.Domain.Config;

namespace SheepBot.Models;

public class Config
{
    public DiscordSettings Discord { get; init; } = new();
    public iRacingSettings iRacing { get; init; } = new();
}