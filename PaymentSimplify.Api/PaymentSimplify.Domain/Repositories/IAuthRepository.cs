using PaymentSimplify.Domain.DTOs;
using PaymentSimplify.Domain.Entities;

namespace PaymentSimplify.Domain.Repositories;

public interface IAuthRepository : IRepository<Auth,Guid>
{
    Task<bool> EmailAlreadyExists(string email);
    Task<AuthDto?> GetUserByEmail(string email);
}