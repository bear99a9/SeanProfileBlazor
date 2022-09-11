using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace SeanProfileBlazor.Services
{
    public class BlogService : IBlogService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthService _authService;

        public BlogService(HttpClient httpClient, IAuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        public async Task<IList<BlogModel>> SaveFile(MultipartFormDataContent content)
        {
            var authToken = await _authService.GetAuthToken();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:1989/api/Blog/upload-file")
            {
                // set request body
                Content = content,
                Headers = { Authorization = new AuthenticationHeaderValue("Bearer", authToken) }
            };

            using var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ServiceResponse<IList<BlogModel>>>();

                return result.Data;

            }

            return new List<BlogModel>();
        }

    }
}
