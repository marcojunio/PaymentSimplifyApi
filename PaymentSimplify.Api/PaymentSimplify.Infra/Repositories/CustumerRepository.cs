using Microsoft.EntityFrameworkCore;
using PaymentSimplify.Application.Common.Interfaces;
using PaymentSimplify.Domain.Entities;
using PaymentSimplify.Domain.Repositories;

namespace PaymentSimplify.Infra.Repositories;

public class CustumerRepository : ICustumerRepository
{
    private readonly IPaymentSimplifyContext _context;

    public CustumerRepository(IPaymentSimplifyContext context)
    {
        _context = context;
    }

    public async Task<Customer?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Customers.FindAsync(id,cancellationToken);
    }

    public async Task Delete(Guid id,CancellationToken cancellationToken)
    {
        var entity = await GetById(id,cancellationToken);
        if (entity != null) _context.Customers.Remove(entity);
    }

    public void Add(Customer entity)
    {
        _context.Customers.Add(entity);
    }

    public void Update(Customer entity)
    {
        _context.Customers.Update(entity);
    }

    public IQueryable<Customer> GetQueryble()
    {
        return _context.Customers.AsQueryable();
    }

    public async Task<bool> DocumentAlreadyExists(string document)
    {
        return await _context.Customers.AsNoTracking().AnyAsync(f => f.Document.Doc == document);
    }
}