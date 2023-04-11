using PaymentSimplify.Application.Common.Interfaces;
using PaymentSimplify.Domain.Entities;
using PaymentSimplify.Domain.Repositories;

namespace PaymentSimplify.Infra.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly IPaymentSimplifyContext _context;

    public TransactionRepository(IPaymentSimplifyContext context)
    {
        _context = context;
    }

    public async Task<Transaction?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Transactions.FindAsync(id,cancellationToken);
    }

    public async Task Delete(Guid id,CancellationToken cancellationToken)
    {
        var entity = await GetById(id,cancellationToken);
        if (entity != null) _context.Transactions.Remove(entity);
    }

    public void Add(Transaction entity)
    {
        _context.Transactions.Add(entity);
    }

    public void Update(Transaction entity)
    {
        _context.Transactions.Update(entity);
    }

    public IQueryable<Transaction> GetQueryble()
    {
        return _context.Transactions.AsQueryable();
    }
}