using Auth.Api.Abstractions;
using Auth.Api.Models;

namespace Auth.Api.Infrastructure;

public class JwtTokenService : ITokenService
{
    public string CreateAccessToken(UserIdentity userIdentity)
    {
        // TODO: Wygeneruj token jwt
        return "abc";
    }
}
