
namespace PaymentSimplify.Domain.Repositories;

public interface IRepository<TEntity,TKey>
{
    public Task<TEntity?> GetById(TKey id,CancellationToken cancellationToken);
    public Task Delete(TKey id,CancellationToken cancellationToken);
    public void Add(TEntity entity);
    public void Update(TEntity entity);
    public IQueryable<TEntity> GetQueryble();
}