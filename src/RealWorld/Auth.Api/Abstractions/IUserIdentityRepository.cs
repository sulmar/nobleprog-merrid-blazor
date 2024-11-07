using Auth.Api.Models;

namespace Auth.Api.Abstractions;

public interface IUserIdentityRepository
{
    Task<UserIdentity> GetByUsername(string username);
}
