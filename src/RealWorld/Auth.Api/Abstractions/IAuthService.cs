using Auth.Api.Models;

namespace Auth.Api.Abstractions;

public interface IAuthService
{
    Task<AuthorizeResult> TryAuthorizeAsync(string username, string password);
}

public class AuthorizeResult
{
    public UserIdentity Identity { get; set; }
    public bool IsSuccess { get; set; }
}