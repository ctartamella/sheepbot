using Microsoft.Extensions.Configuration;
using SheepBot.Domain.Config;

namespace SheepBot.Models;

public class Config
{
    public DiscordSettings Discord { get; init; } = new();
    public iRacingSettings iRacing { get; init; } = new();

    public static Config GetFrom(IConfigurationRoot configurationRoot)
    {
        var config = configurationRoot.Get<Config>();
        if (config is null)
        {
            throw new InvalidProgramException("Could not parse configuration");
        }
        
        config.ValidateConfig();
        return config;
    }
    private void ValidateConfig()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(Discord.Token);
        ArgumentException.ThrowIfNullOrWhiteSpace(iRacing.Email);
        ArgumentException.ThrowIfNullOrWhiteSpace(iRacing.Password);
        ArgumentException.ThrowIfNullOrWhiteSpace(iRacing.BaseUri.ToString());
        
        if (Discord.GuildId <= 0)
        {
            throw new ArgumentException(nameof(Discord.GuildId));
        }
    }
}
