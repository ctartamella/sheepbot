using Microsoft.Extensions.DependencyInjection;

namespace SheepBot.Infrastructure.Helpers;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services;
    }
}