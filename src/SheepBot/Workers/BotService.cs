using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using SheepBot.Domain.Config;

namespace SheepBot.Workers;

public sealed class BotService : ServiceBase
{
    private readonly DiscordSocketClient _client;
    private readonly string _token;

    public BotService(DiscordSettings settings, DiscordSocketClient client, ILogger<BotService> logger)
        : base(logger)
    {
        ArgumentNullException.ThrowIfNull(settings);
        ArgumentNullException.ThrowIfNull(client);

        _token = settings.Token;
        _client = client;
        _client.Log += Log;
    }
    
    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        await _client.LoginAsync(TokenType.Bot, _token).ConfigureAwait(false);
        await _client.StartAsync().ConfigureAwait(false);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _client.StopAsync().ConfigureAwait(false);
        await _client.LogoutAsync().ConfigureAwait(false);
    }
}