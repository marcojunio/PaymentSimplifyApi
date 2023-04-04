using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PaymentSimplify.Application.Common.Interfaces;
using PaymentSimplify.Common.Settings;
using PaymentSimplify.Infra.File;
using PaymentSimplify.Infra.Persistence;
using PaymentSimplify.Infra.Services;

namespace PaymentSimplify.Infra;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        AppSettings appSettings)
    {
        //auth
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddDbContext<PaymentSimplifyContext>(options =>
        {
            options.UseMySQL(appSettings.SettingsDb.ConnectionString);
        });

        services.AddScoped<IUnitOfWork>(f => f.GetRequiredService<UnitOfWork>());

        //utils
        services.AddScoped<IDateTime, DateTimeService>();
        services.AddScoped<ICsvBuilderFile, CsvBuilderFile>();
        
        //services
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
}