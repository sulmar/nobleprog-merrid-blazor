using Auth.Api.Abstractions;
using Auth.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace Auth.Api.Infrastructure;

public class AuthService : IAuthService
{
    private readonly IUserIdentityRepository repository;
    private readonly IPasswordHasher<UserIdentity> passwordHasher;

    public AuthService(IUserIdentityRepository repository, IPasswordHasher<UserIdentity> passwordHasher)
    {
        this.repository = repository;
        this.passwordHasher = passwordHasher;
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
