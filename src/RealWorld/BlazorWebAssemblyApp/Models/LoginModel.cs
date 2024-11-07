namespace BlazorWebAssemblyApp.Models;

public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}


public class CreateTokenResponse
{
    public string AccessToken { get; set; }
}