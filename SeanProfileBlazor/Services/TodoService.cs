using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SeanProfileBlazor.Services
{
    public class TodoService : ITodoService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localstorage;
        private readonly IAuthService _authService;

        public TodoService(HttpClient httpClient, ILocalStorageService localstorage, IAuthService authService)
        {
            _httpClient = httpClient;
            _localstorage = localstorage;
            _authService = authService;
        }

        public async Task<IEnumerable<TodoModel>> GetTodos()
        {
            var response = await JsonSerializer.DeserializeAsync<IEnumerable<TodoModel>>
                (await _httpClient.GetStreamAsync($"api/Todo/GetAllTodos"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            if (response != null && response.Any())
            {
                return response;
            }

            return new List<TodoModel>();

        }

        public async Task<TodoModel> GetTodoById(int id)
        {
            var authToken = await _authService.GetAuthToken();

            var request = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:1989/api/Todo/GetTodoById/{id}")
            {
                Headers = { Authorization = new AuthenticationHeaderValue("Bearer", authToken) }
            };


            using var response = await _httpClient.SendAsync(request);

            var result = await response.Content.ReadFromJsonAsync<TodoModel>();

            if (result != null)
            {
                return result;
            }

            return new TodoModel();
        }

        public async Task<bool> AddTodo(TodoModel todo)
        {
            var authToken = await _authService.GetAuthToken();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:1989/api/Todo/CreateTodo")
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

        public async Task<bool> UpdateTodo(TodoModel todo)
        {
            var response = await _httpClient.PutAsync("api/Todo/UpdateTodoById",
                new StringContent(JsonSerializer.Serialize(todo), System.Text.Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<bool>(await response.Content.ReadAsStreamAsync());
            }

            return false;
        }

        public async Task<bool> DeleteTodo(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Todo/DeleteTodoById/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<bool>(await response.Content.ReadAsStreamAsync());
            }

            return false;
        }

    }
}
