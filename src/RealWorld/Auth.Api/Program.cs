using Auth.Api.Abstractions;
using Auth.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapPost("/api/token/create", async (LoginRequest request, IAuthService authService, ITokenService tokenService) =>
{
     var authResult = await authService.TryAuthorizeAsync(request.Username, request.Password);

    if (authResult.IsSuccess)
    {
        var accessToken = tokenService.CreateAccessToken(authResult.Identity);

        var result = new
        {
            AccessToken = accessToken,            
        };

        return Results.Ok(result);
        
    }

    return Results.Unauthorized();
});


app.Run();

