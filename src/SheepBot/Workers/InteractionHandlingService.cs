using System.Reflection;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;

namespace SheepBot.Workers;

public class InteractionHandlingService : ServiceBase
{
    private readonly DiscordSocketClient _discord;
    private readonly InteractionService _interactions;
    private readonly IServiceProvider _services;

    public InteractionHandlingService(
        DiscordSocketClient discord,
        InteractionService interactions,
        IServiceProvider services,
        ILogger<InteractionHandlingService> logger
    ) : base(logger)
    {
        _discord = discord;
        _interactions = interactions;
        _services = services;

        _interactions.Log += Log;
    }
    
    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        _discord.Ready += () => _interactions.RegisterCommandsGloballyAsync();
        _discord.InteractionCreated += OnInteractionAsync;

        await _interactions.AddModulesAsync(Assembly.GetEntryAssembly(), _services).ConfigureAwait(false);
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _interactions.Dispose();
        return Task.CompletedTask;
    }
    
    private async Task OnInteractionAsync(SocketInteraction interaction)
    {
        try
        {
            var context = new SocketInteractionContext(_discord, interaction);
            var result = await _interactions.ExecuteCommandAsync(context, _services).ConfigureAwait(false);

            if (!result.IsSuccess)
                await context.Channel.SendMessageAsync(result.ToString()).ConfigureAwait(false);
        }
        catch
        {
            if (interaction.Type == InteractionType.ApplicationCommand)
            {
                await interaction
                    .GetOriginalResponseAsync()
                    .ContinueWith(msg => msg.Result.DeleteAsync())
                    .ConfigureAwait(false);
            }
        }
    }
}