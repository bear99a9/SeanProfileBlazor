using System.Net.Http.Json;

namespace SeanProfileBlazor.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<string>> Login(UserLogin userLogin)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/login", userLogin);

            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<int>> RegisterUser(UserRegister userRegister)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/register", userRegister);

            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }
    }
}
