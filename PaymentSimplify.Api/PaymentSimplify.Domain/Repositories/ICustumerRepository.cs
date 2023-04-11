using PaymentSimplify.Domain.Entities;

namespace PaymentSimplify.Domain.Repositories;

public interface ICustumerRepository : IRepository<Customer,Guid>
{
    Task<bool> DocumentAlreadyExists(string document);
}