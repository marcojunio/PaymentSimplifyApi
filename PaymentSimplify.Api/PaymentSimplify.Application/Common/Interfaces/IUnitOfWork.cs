using System.Data.Common;

namespace PaymentSimplify.Application.Common.Interfaces;

public interface IUnitOfWork
{
    DbTransaction BeginTransaction();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}