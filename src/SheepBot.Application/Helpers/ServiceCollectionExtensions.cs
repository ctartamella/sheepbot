using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SheepBot.Application.Interfaces;

namespace SheepBot.Application.Helpers;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, string connectionString)
    {
        var assembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddAutoMapper(_ => { }, assembly);
        
        services.AddSingleton<IConnectionFactory, ConnectionFactory>(_ => new ConnectionFactory(connectionString));
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
        });

        services.AddValidatorsFromAssembly(assembly);
        
        return services;
    }
}