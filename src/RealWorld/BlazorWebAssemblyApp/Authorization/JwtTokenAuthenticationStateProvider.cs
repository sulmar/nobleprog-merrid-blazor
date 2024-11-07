using BlazorWebAssemblyApp.Models;
using BlazorWebAssemblyApp.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazorWebAssemblyApp.Authorization;

public class JwtTokenAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IAuthApiService api;
    private readonly LocalStorage localStorage;

    public JwtTokenAuthenticationStateProvider(IAuthApiService api, LocalStorage localStorage)
    {
        this.api = api;
        this.localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var state = new AuthenticationState(new ClaimsPrincipal());

        var accesstoken = await localStorage.GetItem("access-token");

        if (!string.IsNullOrEmpty(accesstoken))
        {
            string secretKey = "your-256-bit-secret-your-256-bit-secret-your-256-bit-secret";
            var key = Encoding.ASCII.GetBytes(secretKey);

            // dotnet add package System.IdentityModel.Tokens.Jwt

            var tokenHandler = new JwtSecurityTokenHandler();

            if (tokenHandler.CanReadToken(accesstoken))
            {
                var parameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "Abc",
                    ValidAudience = "Xyz",
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                try
                {
                    var jwtToken = tokenHandler.ReadJwtToken(accesstoken);

                    tokenHandler.ValidateToken(accesstoken, parameters, out var validatedToken);

                    var identity = new ClaimsIdentity(jwtToken.Claims, "Bearer");
                    var principal = new ClaimsPrincipal(identity);

                    NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
                }
                catch (Exception ex)
                {
                    await LogoutAsync();
                }
            }            
        }

        return state;
    }


    public async Task LoginAsync(LoginModel model)
    {
        var accessToken = await api.CreateTokenAsync(model);

        await localStorage.SetItem("access-token", accessToken);

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task LogoutAsync()
    {
        await localStorage.RemoveItem("access-token");

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());


    }
}
