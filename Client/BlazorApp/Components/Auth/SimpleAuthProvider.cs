using System.Security.Claims;
using System.Text.Json;
using DTOs;
using Microsoft.AspNetCore.Components.Authorization;

public class SimpleAuthProvider : AuthenticationStateProvider
{
    private readonly HttpClient client;
    private ClaimsPrincipal? currentClaimsPrincipal;

    public SimpleAuthProvider(HttpClient client)
    {
        this.client = client;
    }

    public async Task Login(string username, string password)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync(
            "auth/login",
            new LoginRequest(username, password)
        );

        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        UserDTO userDTO = JsonSerializer.Deserialize<UserDTO>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, userDTO.Username),
            new Claim("Id", userDTO.UserId.ToString()),
        };

        ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth");
        currentClaimsPrincipal = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(currentClaimsPrincipal))
        );
    }
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return Task.FromResult(new AuthenticationState(currentClaimsPrincipal ?? new()));
    }

    public void Logout()
    {
        currentClaimsPrincipal = new();
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(currentClaimsPrincipal)));
    }
}