using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SheepBot.Application.Helpers;
using SheepBot.Infrastructure.Helpers;
using SheepBot.iRacing.Client.Helpers;
using SheepBot.Models;
using SheepBot.Workers;

// ReSharper disable StringLiteralTypo

var configurationRoot = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false, true)
    .AddEnvironmentVariables("Sheepbot")
    .AddUserSecrets(typeof(Program).Assembly)
    .Build();

// ReSharper restore StringLiteralTypo

var config = configurationRoot.Get<Config>();

if (config is null)
{
    throw new InvalidDataException("Can not parse configuration. Exiting.");
}

var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(builder => builder.AddConsole())
    .ConfigureServices(services =>
    {
        var connectionString = configurationRoot.GetConnectionString("Default") ?? throw new InvalidDataException();

        services
            .AddSingleton(config.Discord)
            .AddSingleton(config.iRacing)
            .AddSingleton(new InteractionServiceConfig
            {
                DefaultRunMode = RunMode.Async,
                InteractionCustomIdDelimiters = new[] { ';' }
            })
            .AddIRacingClient(config.iRacing)
            .AddApplication(connectionString)
            .AddInfrastructure()
            .AddSingleton<DiscordSocketClient>()
            .AddSingleton<InteractionService>()
            //.AddHostedService<TrackWorker>()
            .AddHostedService<InteractionHandlingService>()
            .AddHostedService<BotService>();
    })
    .Build();

await host.RunAsync().ConfigureAwait(false);
    