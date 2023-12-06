using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SheepBot.Infrastructure.Context;

namespace SheepBot.Infrastructure.Helpers;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<SheepContext>(opt =>
        {
            opt.UseSqlServer(connectionString, x => x.UseNetTopologySuite());
        });
        return services;
    }
}