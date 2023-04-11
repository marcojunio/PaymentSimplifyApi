using PaymentSimplify.Domain.Entities;

namespace PaymentSimplify.Domain.Repositories;

public interface ITransactionRepository : IRepository<Transaction,Guid>
{
    
}