namespace SheepBot.Domain.Config;

public class DiscordSettings
{
    public ulong GuildId { get; init; }
    public string Token { get; init; } = null!;
}