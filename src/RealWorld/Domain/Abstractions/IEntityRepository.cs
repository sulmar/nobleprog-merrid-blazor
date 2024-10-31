using Domain.Models;

namespace Domain.Abstractions;

public interface IEntityRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetAsync(int id);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task RemoveAsync(int id);
}


