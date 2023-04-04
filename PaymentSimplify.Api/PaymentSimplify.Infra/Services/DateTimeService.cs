using PaymentSimplify.Application.Common.Interfaces;

namespace PaymentSimplify.Infra.Services;

public class DateTimeService : IDateTime
{
    public DateTime UtcNow => DateTime.UtcNow;
}