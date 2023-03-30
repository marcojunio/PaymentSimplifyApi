using Microsoft.Extensions.DependencyInjection;

namespace PaymentSimplify.Common.Results;

public static class Extensions
{
    public static void AddResultService(this IServiceCollection services) =>
        services.AddSingleton<IResultService, ResultService>();
}