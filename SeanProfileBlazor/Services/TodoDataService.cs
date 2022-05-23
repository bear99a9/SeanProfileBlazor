using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SeanProfileBlazor.Services
{
    public class TodoDataService : ITodoDataService
    {
        private readonly HttpClient _httpClient;

        public TodoDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
            var response = await JsonSerializer.DeserializeAsync<TodoModel>
                (await _httpClient.GetStreamAsync($"api/Todo/Todo/GetTodoById/{id}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            if (response != null)
            {
                return response;
            }

            return new TodoModel();
        }

        public async Task<bool> AddTodo(TodoModel todo)
        {
            var response = await _httpClient.PostAsync("api/Todo/CreateTodo",
                new StringContent(JsonSerializer.Serialize(todo), System.Text.Encoding.UTF8, "application/json"));

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
