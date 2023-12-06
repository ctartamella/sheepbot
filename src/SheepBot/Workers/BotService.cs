using Discord;
using Discord.Net;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using SheepBot.Domain.Config;

namespace SheepBot.Workers;

public sealed class BotService : BackgroundService
{
    private readonly DiscordSocketClient _client;
    private readonly string _token;
    private readonly ulong _guildId;

    public BotService(DiscordSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        _token = settings.Token;
        _guildId = settings.GuildId;
        _client = new DiscordSocketClient();
        _client.Log += Log;
        _client.Ready += Client_Ready;
    }

    private Task Log(LogMessage arg)
    {
        Console.WriteLine(arg.Message);
        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _client.Log -= Log;
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

    private async Task Client_Ready()
    {
        var guildCommand = new SlashCommandBuilder()
            .WithName("track")
            .WithDescription("Access iRacing track data.")
            .AddOption(new SlashCommandOptionBuilder()
                .WithName("info")
                .WithDescription("Gets information about a track")
                .WithType(ApplicationCommandOptionType.SubCommand)
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName("set")
                    .WithDescription("Sets the field A")
                    .WithType(ApplicationCommandOptionType.SubCommand)
                    .AddOption("value", ApplicationCommandOptionType.String, "the value to set the field",
                        isRequired: true)
                ).AddOption(new SlashCommandOptionBuilder()
                    .WithName("get")
                    .WithDescription("Gets the value of field A.")
                    .WithType(ApplicationCommandOptionType.SubCommand)
                )
            );

        try
        {
            await _client.Rest
                .CreateGuildCommand(guildCommand.Build(), _guildId)
                .ConfigureAwait(false);
        }
        catch (HttpException exception)
        {
            var json = JsonConvert.SerializeObject(exception, Formatting.Indented);
            Console.WriteLine(json);
        }
    }
}