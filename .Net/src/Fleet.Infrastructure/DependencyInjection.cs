using Fleet.Application.Common.Interfaces;
using Fleet.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fleet.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") 
                               ?? "Server=localhost;Database=FleetSignalR;Trusted_Connection=True;TrustServerCertificate=True;";

        services.AddDbContext<FleetDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IFleetDbContext>(sp => sp.GetRequiredService<FleetDbContext>());

        return services;
    }
}
