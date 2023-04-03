using System.Data.Common;

namespace PaymentSimplify.Application.Interfaces;

public interface IUnitOfWork
{
    DbTransaction BeginTransaction();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}