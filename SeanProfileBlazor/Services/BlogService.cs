using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SeanProfileBlazor.Services
{
    public class BlogService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthService _authService;

        public BlogService(HttpClient httpClient, IAuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        public async Task<bool> SaveFile(TodoModel todo)
        {
            var authToken = await _authService.GetAuthToken();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:1989/api/Blog/upload-file")
            {
                // set request body
                Content = new StringContent(JsonSerializer.Serialize(todo), Encoding.UTF8, "application/json"),
                Headers = { Authorization = new AuthenticationHeaderValue("Bearer", authToken) }
            };

            using var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<bool>(await response.Content.ReadAsStreamAsync());
            }

            return false;
        }

    }
}
