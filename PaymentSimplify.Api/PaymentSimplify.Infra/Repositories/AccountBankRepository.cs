using PaymentSimplify.Application.Common.Interfaces;
using PaymentSimplify.Domain.Entities;
using PaymentSimplify.Domain.Repositories;

namespace PaymentSimplify.Infra.Repositories;

public class AccountBankRepository : IAccountBankRepository
{
    private readonly IPaymentSimplifyContext _context;

    public AccountBankRepository(IPaymentSimplifyContext context)
    {
        _context = context;
    }

    public async Task<AccountBank?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _context.AccountBanks.FindAsync(id,cancellationToken);
    }

    public async Task Delete(Guid id,CancellationToken cancellationToken)
    {
        var entity = await GetById(id,cancellationToken);
        if (entity != null) _context.AccountBanks.Remove(entity);
    }

    public void Add(AccountBank entity)
    {
        _context.AccountBanks.Add(entity);
    }

    public void Update(AccountBank entity)
    {
        _context.AccountBanks.Update(entity);
    }

    public IQueryable<AccountBank> GetQueryble()
    {
        return _context.AccountBanks.AsQueryable();
    }
}