using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWebAssemblyApp;
using BlazorWebAssemblyApp.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using BlazorWebAssemblyApp.Services;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// dotnet add package Microsoft.Extensions.Http
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddHttpClient<IAuthApiService, AuthApiService>((client) => client.BaseAddress = new Uri("https://localhost:7027"));

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<JwtTokenAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>( sp => sp.GetRequiredService<JwtTokenAuthenticationStateProvider>());

builder.Services.AddScoped<AuthApiService>();

builder.Services.AddScoped<LocalStorage>();

await builder.Build().RunAsync();
