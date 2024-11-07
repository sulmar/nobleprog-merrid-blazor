using Auth.Api.Abstractions;
using Auth.Api.Models;

namespace Auth.Api.Infrastructure;

public class FakeUserIdentityRepository : IUserIdentityRepository
{
    private readonly IDictionary<string, UserIdentity> users = new Dictionary<string, UserIdentity>();

    public FakeUserIdentityRepository(IEnumerable<UserIdentity> users)
    {
        this.users = users.ToDictionary(p => p.Username);
    }

    public Task<UserIdentity> GetByUsername(string username)
    {
        return Task.FromResult(users[username]);
    }
}
