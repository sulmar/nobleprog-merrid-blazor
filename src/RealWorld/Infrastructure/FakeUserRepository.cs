using Domain.Abstractions;
using Domain.Models;

namespace Infrastructure;

public class FakeUserRepository : FakeEntityRepository<User>, IUserRepository
{
    public FakeUserRepository(IEnumerable<User> entities) : base(entities)
    {
    }

    public override Task RemoveAsync(int id)
    {
        if (id == 0)
        {
            throw new ArgumentException();
        }

        return base.RemoveAsync(id);
    }

    public Task<User> GetByPesel(string pesel) => Task.FromResult(_entities.Values.SingleOrDefault(u => u.Pesel == pesel));
}