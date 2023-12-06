using Microsoft.Extensions.DependencyInjection;
using Refit;
using SheepBot.Domain.Config;
using SheepBot.iRacing.Client.Interfaces;
using SheepBot.iRacing.Client.Services;

namespace SheepBot.iRacing.Client.Helpers;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIRacingClient(this IServiceCollection services, iRacingSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        services.AddHttpClient();
        services.AddHttpClient("NoAuth", c =>
        {
            c.BaseAddress = settings.BaseUri;
        });
        
        services.AddSingleton<iRacingHttpClient>(provider =>
        {
            var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
            var socketHandler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(5)
            };
            
            var handler = new iRacingAuthorizationHandler(httpClientFactory, settings, socketHandler);
            return new iRacingHttpClient(handler)
            {
                BaseAddress = settings.BaseUri
            };
        });

        services.AddTransient<iRacingApi>(provider =>
        {
            var client = provider.GetRequiredService<iRacingHttpClient>();
            
            return RestService.For<iRacingApi>(client);
        });
        
        services.AddTransient<IiRacingService, iRacingService>();
        
        return services;
    }
}