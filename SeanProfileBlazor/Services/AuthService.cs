using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace SeanProfileBlazor.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localstorage;

        public AuthService(HttpClient httpClient, ILocalStorageService localstorage)
        {
            _httpClient = httpClient;
            _localstorage = localstorage;
        }

        public async Task<ServiceResponse<bool>> ChangePassword(UserChangePassword changePassword)
        {
            var authToken = await GetAuthToken();
            
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:1989/api/auth/change-password")
            {
                // set request body
                Content = new StringContent(JsonSerializer.Serialize(changePassword), Encoding.UTF8, "application/json")
            };

            // add authorization header
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            // add custom http header
            //request.Headers.Add("My-Custom-Header", "foobar");

            // send request
            using var result = await _httpClient.SendAsync(request);

            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
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

        public async Task<string> GetAuthToken()
        {
            var authToken =  await _localstorage.GetItemAsStringAsync("authToken");
            authToken.Replace("\"", "");
            return authToken.Replace("'", "");
        }
    }
}
