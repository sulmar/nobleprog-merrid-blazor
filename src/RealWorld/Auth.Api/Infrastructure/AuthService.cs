using Auth.Api.Abstractions;
using Auth.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace Auth.Api.Infrastructure;

public class AuthService : IAuthService
{
    private readonly IPasswordHasher<UserIdentity> passwordHasher;
    private readonly IUserIdentityRepository repository;

    public AuthService(IUserIdentityRepository repository)
    {
        this.repository = repository;
    }

    public async Task<AuthorizeResult> TryAuthorizeAsync(string username, string password)
    {
        var userIdentity = await repository.GetByUsername(username);

        if (userIdentity != null && passwordHasher.VerifyHashedPassword(userIdentity, userIdentity.HashedPassword, password) == PasswordVerificationResult.Success)
        {
            return new AuthorizeResult { Identity = userIdentity, IsSuccess = true };
        }
        else
        {
            return new AuthorizeResult { IsSuccess = false };
        }
    }
}
