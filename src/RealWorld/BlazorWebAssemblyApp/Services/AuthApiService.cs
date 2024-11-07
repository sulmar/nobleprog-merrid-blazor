using BlazorWebAssemblyApp.Models;
using System.Net.Http.Json;

namespace BlazorWebAssemblyApp.Services;

public interface IAuthApiService
{
    Task<string> CreateTokenAsync(LoginModel model);
}

public class AuthApiService : IAuthApiService
{
    private readonly HttpClient client;

    public AuthApiService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<string> CreateTokenAsync(LoginModel model)
    {
        var response = await client.PostAsJsonAsync("api/token/create", model);

        var createTokenResponse = await response.Content.ReadFromJsonAsync<CreateTokenResponse>();

        return createTokenResponse.AccessToken;
    }
}
