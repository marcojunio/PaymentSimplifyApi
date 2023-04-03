using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PaymentSimplify.Application.Interfaces;
using PaymentSimplify.Common.Settings;
using PaymentSimplify.Infra.Persistence;

namespace PaymentSimplify.Infra;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,AppSettings appSettings)
    {
        services.AddDbContext<PaymentSimplifyContext>(options =>
        {
            options.UseMySQL(appSettings.SettingsDb.ConnectionString);
        });

        services.AddScoped<IUnitOfWork>(f => f.GetRequiredService<UnitOfWork>());

        return services;
    }
}