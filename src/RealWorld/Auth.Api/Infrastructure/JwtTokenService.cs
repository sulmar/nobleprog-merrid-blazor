using Auth.Api.Abstractions;
using Auth.Api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Api.Infrastructure;

// dotnet add package System.IdentityModel.Tokens.Jwt

public class JwtTokenService : ITokenService
{
    public string CreateAccessToken(UserIdentity userIdentity)
    {
        Claim[] claims = [
                new Claim(ClaimTypes.Name, userIdentity.Username),
                //new Claim(ClaimTypes.Email, userIdentity.Email),
                new Claim(ClaimTypes.NameIdentifier, userIdentity.Username),

                new Claim(ClaimTypes.Role, "admin"),
                new Claim(ClaimTypes.Role, "developer"),

                new Claim(JwtRegisteredClaimNames.Sub, userIdentity.Username),
                new Claim(JwtRegisteredClaimNames.Name, userIdentity.Username),
                new Claim(JwtRegisteredClaimNames.Email, userIdentity.Email),
            ];

        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims);

        string secretKey = "your-256-bit-secret-your-256-bit-secret-your-256-bit-secret";

        var key = Encoding.ASCII.GetBytes(secretKey);

        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

        JwtSecurityToken securityToken = new JwtSecurityToken(
            issuer: "Abc",
            audience: "Xyz",
            expires: DateTime.UtcNow.AddMinutes(5),
            claims: claims,
            signingCredentials: signingCredentials);

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);


        return token;
    }
}
