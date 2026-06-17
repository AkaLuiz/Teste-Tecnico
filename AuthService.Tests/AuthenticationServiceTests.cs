using System.Net.Http.Json;
using AuthService.Enums;
using Microsoft.AspNetCore.Mvc.Testing;

namespace AuthService.Tests;

public class AuthenticationServiceTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public AuthenticationServiceTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Login_DeveRetornarTokenValido()
    {
        var request = new
        {
            Email = "admin@example.com",
            Senha = "admin123"
        };

        var response = await _client.PostAsJsonAsync("/auth/login", request);
        response.EnsureSuccessStatusCode();

        var LoginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

        Assert.NotNull(LoginResponse);
        Assert.False(string.IsNullOrEmpty(LoginResponse.Token));
    }

    [Fact]
    public async Task Me_DeveRetornarUsuarioLogado()
    {
        // Primeiro, faça login para obter o token
        var loginRequest = new
        {
            Email = "admin@example.com",
            Senha = "admin123"
        };
        var loginResponse = await _client.PostAsJsonAsync("/auth/login", loginRequest);
        loginResponse.EnsureSuccessStatusCode();

        var loginResult = await loginResponse.Content.ReadFromJsonAsync<LoginResponse>();
        Assert.NotNull(loginResult);
        Assert.False(string.IsNullOrWhiteSpace(loginResult.Token));

        // Agora, use o token para fazer a requisição ao endpoint /auth/me
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", loginResult.Token);
        var meResponse = await _client.GetAsync("/auth/me");
        meResponse.EnsureSuccessStatusCode();
        var meResult = await meResponse.Content.ReadFromJsonAsync<Usuario>();
        Assert.NotNull(meResult);
        Assert.Equal("admin@example.com",meResult.Email);
        Assert.Equal(UsuarioPapel.Admin, meResult.UsuarioPapel);
    }
}
