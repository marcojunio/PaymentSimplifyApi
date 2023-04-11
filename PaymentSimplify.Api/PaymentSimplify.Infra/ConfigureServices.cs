using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PaymentSimplify.Application.Common.Interfaces;
using PaymentSimplify.Common.Settings;
using PaymentSimplify.Domain.Repositories;
using PaymentSimplify.Infra.File;
using PaymentSimplify.Infra.Persistence;
using PaymentSimplify.Infra.Repositories;
using PaymentSimplify.Infra.Services;

namespace PaymentSimplify.Infra;

public static class ConfigureServices
{
    public static void AddInfrastructureServices(this IServiceCollection services,
        AppSettings appSettings)
    {
        services.AddDbContext<PaymentSimplifyContext>(options =>
        {
            options.UseMySQL(appSettings.SettingsDb.ConnectionString);
        });

        services.AddScoped<IPaymentSimplifyContext>(provider => provider.GetRequiredService<PaymentSimplifyContext>());

        services.AddScoped<IUnitOfWork,UnitOfWork>();

        //repositories
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IAccountBankRepository, AccountBankRepository>();
        services.AddScoped<ICustumerRepository, CustumerRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        
        //utils
        services.AddScoped<IDateTime, DateTimeService>();
        services.AddScoped<ICsvBuilderFile, CsvBuilderFile>();
        services.AddSingleton<IHashService, HashService>();
        services.AddSingleton<IJwtService, JwtService>();

    }
}