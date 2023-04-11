using Microsoft.EntityFrameworkCore;
using PaymentSimplify.Application.Common.Interfaces;
using PaymentSimplify.Domain.DTOs;
using PaymentSimplify.Domain.Entities;
using PaymentSimplify.Domain.Repositories;

namespace PaymentSimplify.Infra.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly IPaymentSimplifyContext _context;

    public AuthRepository(IPaymentSimplifyContext context)
    {
        _context = context;
    }

    public async Task<Auth?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Auths.FindAsync(id,cancellationToken);
    }

    public async Task Delete(Guid id,CancellationToken cancellationToken)
    {
        var entity = await GetById(id,cancellationToken);
        if (entity != null) _context.Auths.Remove(entity);
    }

    public void Add(Auth entity)
    {
        _context.Auths.Add(entity);
    }

    public void Update(Auth entity)
    {
        _context.Auths.Update(entity);
    }

    public IQueryable<Auth> GetQueryble()
    {
        return _context.Auths.AsQueryable();
    }

    public async Task<bool> EmailAlreadyExists(string email)
    {
        return await _context.Auths.AsNoTracking().AnyAsync(f => f.Email.Addreess == email);
    }

    public async Task<AuthDto?> GetUserByEmail(string email)
    {
        return await _context.Auths
            .AsNoTracking()
            .Where(f => f.Email.Addreess == email)
            .Select(s => new AuthDto()
            {
                Id = s.Id.ToString(),
                Email = s.Email.Addreess,
                Password = s.Password,
                Salt = s.Salt
            })
            .FirstOrDefaultAsync();
    }
}