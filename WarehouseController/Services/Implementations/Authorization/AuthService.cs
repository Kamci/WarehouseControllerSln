using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace WarehouseController.Services.Implementations.Authorization
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        public static string? JwtToken { get; private set; }

        public AuthService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7167/")
            };
        }

        public async Task<bool> LoginAsync(string login, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("api/User/login", new
            {
                login,
                password
            });

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();               
                var result = JsonSerializer.Deserialize<TokenResponse>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                JwtToken = result?.Token;
                SessionService.LoggedInUserLogin = result.Login;
                SessionService.LoggedInUserRole = result.Role;
                SessionService.LoggedInUserId = result.UserId;
                return true;
            }

            return false;
        }

        private class TokenResponse
        {
            public string Token { get; set; }
            public string Login { get; set; }
            public string Role { get; set; }
            public int UserId { get; set; }
        }
    }
}
