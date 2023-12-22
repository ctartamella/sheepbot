using Discord;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SheepBot.Workers;

public abstract class ServiceBase : IHostedService
{
    protected readonly ILogger Logger;
    
    protected ServiceBase(ILogger logger)
    {
        Logger = logger;
    }
    
    public abstract Task StartAsync(CancellationToken cancellationToken);
    public abstract Task StopAsync(CancellationToken cancellationToken);
    
    protected Task Log(LogMessage msg)
    {
        switch (msg.Severity)
        {
            case LogSeverity.Verbose:
                Logger.LogInformation(msg.ToString());
                break;

            case LogSeverity.Info:
                Logger.LogInformation(msg.ToString());
                break;

            case LogSeverity.Warning:
                Logger.LogWarning(msg.ToString());
                break;

            case LogSeverity.Error:
                Logger.LogError(msg.ToString());
                break;

            case LogSeverity.Critical:
                Logger.LogCritical(msg.ToString());
                break;
        }
        return Task.CompletedTask;
    }
}