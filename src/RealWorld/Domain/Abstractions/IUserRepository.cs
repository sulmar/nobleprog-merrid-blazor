using Domain.Models;

namespace Domain.Abstractions;

public interface IUserRepository : IEntityRepository<User>
{
    Task<User> GetByPesel(string pesel);
}


