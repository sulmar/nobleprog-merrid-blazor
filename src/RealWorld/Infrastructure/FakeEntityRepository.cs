using Domain.Abstractions;
using Domain.Models;

namespace Infrastructure;

public class FakeEntityRepository<TEntity> : IEntityRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly IDictionary<int, TEntity> _entities;

    public FakeEntityRepository(IEnumerable<TEntity> entities)
    {
        _entities = entities.ToDictionary(c => c.Id);
    }

    public Task AddAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await Task.FromResult(_entities.Values.ToList());
    }

    public Task<TEntity> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public virtual Task RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }
}
