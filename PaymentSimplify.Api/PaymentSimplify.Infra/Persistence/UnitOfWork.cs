using System.Data.Common;
using Microsoft.EntityFrameworkCore.Storage;
using PaymentSimplify.Application.Interfaces;

namespace PaymentSimplify.Infra.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly PaymentSimplifyContext _dbContext;

    public UnitOfWork(PaymentSimplifyContext dbContext)
    {
        _dbContext = dbContext;
    }

    public DbTransaction BeginTransaction()
    {
        var transaction = _dbContext.Database.BeginTransaction();
        return transaction.GetDbTransaction();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}