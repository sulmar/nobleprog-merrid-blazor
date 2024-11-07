using Auth.Api.Abstractions;
using Auth.Api.Infrastructure;
using Auth.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserIdentityRepository, FakeUserIdentityRepository>();
builder.Services.AddScoped<IEnumerable<UserIdentity>>(sp =>
{
    var passwordHasher = sp.GetRequiredService<IPasswordHasher<UserIdentity>>();

    return new List<UserIdentity>
    {
        new UserIdentity { Username = "john", FirstName = "John", LastName = "Smith", Email = "john@domain.com", HashedPassword = passwordHasher.HashPassword(null, "123") },
        new UserIdentity { Username = "kate", FirstName = "Kate", LastName = "Smith", Email = "kate@domain.com", HashedPassword = passwordHasher.HashPassword(null, "123")  },
        new UserIdentity { Username = "bob", FirstName = "Bob", LastName = "Smith" , Email = "bob@domain.com", HashedPassword = passwordHasher.HashPassword(null, "123") },
    };
});

//builder.Services.AddScoped<IPasswordHasher<UserIdentity>, PasswordHasher<UserIdentity>>();
builder.Services.AddScoped<IPasswordHasher<UserIdentity>, BCryptPasswordHasher<UserIdentity>>();

builder.Services.AddScoped<ITokenService, JwtTokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapPost("/api/token/create", async ([FromForm] LoginRequest request, IAuthService authService, ITokenService tokenService) =>
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
}).DisableAntiforgery();


app.Run();

