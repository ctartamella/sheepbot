using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SheepBot.Application.Interfaces;
using SheepBot.Infrastructure.Context;

namespace SheepBot.Application.Helpers;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, string connectionString)
    {
        var assembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddAutoMapper(_ => { }, assembly);
        
        services.AddDbContext<SheepContext>((provider, opt) =>
        {
            var config = provider.GetRequiredService<IConnectionFactory>();
            opt.UseSqlServer(config.ConnectionString, x => x.UseNetTopologySuite());
        });
        
        services.AddSingleton<IConnectionFactory, ConnectionFactory>(_ => new ConnectionFactory(connectionString));
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
        });

        services.AddValidatorsFromAssembly(assembly);
        
        return services;
    }
}