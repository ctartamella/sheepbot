using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SheepBot.Application.Helpers;
using SheepBot.Infrastructure.Helpers;
using SheepBot.iRacing.Client.Helpers;
using SheepBot.SyncWorkers.Models;
using SheepBot.SyncWorkers.Workers;

var configurationRoot = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false, true)
    .AddUserSecrets(typeof(Program).Assembly)
    .Build();

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
            .AddIRacingClient(config.iRacing)
            .AddApplication(connectionString)
            .AddInfrastructure(connectionString)
            .AddHostedService<TrackWorker>();
    })
    .Build();

await host.RunAsync().ConfigureAwait(false);