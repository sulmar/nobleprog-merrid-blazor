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
        var tokenHandler = new JwtSecurityTokenHandler();

        Claim[] claims = [
                //new Claim(ClaimTypes.Name, userIdentity.Username),
                //new Claim(ClaimTypes.Email, userIdentity.Email),
                //new Claim(ClaimTypes.NameIdentifier, userIdentity.Username),

                new Claim(JwtRegisteredClaimNames.Sub, userIdentity.Username),
                new Claim(JwtRegisteredClaimNames.Name, userIdentity.Username),
                new Claim(JwtRegisteredClaimNames.Email, userIdentity.Email),
            ];

        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims);

        string secretKey = "your-256-bit-secret-your-256-bit-secret-your-256-bit-secret";

        var key = Encoding.ASCII.GetBytes(secretKey);

        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Expires = DateTime.UtcNow.AddMinutes(5),
            SigningCredentials = signingCredentials
        };

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        var token = tokenHandler.WriteToken(securityToken);


        return token;
    }
}
