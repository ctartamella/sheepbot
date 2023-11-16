using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;

namespace SheepBot;

public sealed class BotService : BackgroundService
{
    private readonly DiscordSocketClient _client;
    private readonly string _token;
    
    public BotService()
    {
        _token = Environment.GetEnvironmentVariable("BOT_TOKEN") 
                 ?? throw new ArgumentException("BOT_TOKEN must be defined.");

        _client = new DiscordSocketClient();
        _client.Log += Log;
    }

    public override void Dispose()
    {   
        _client.Dispose();
        
        base.Dispose();
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _client.LoginAsync(TokenType.Bot, _token).ConfigureAwait(false);
        await _client.StartAsync().ConfigureAwait(false);

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(2000, stoppingToken).ConfigureAwait(false);
        }

        await _client.LogoutAsync().ConfigureAwait(false);
    }
    
    private static Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}