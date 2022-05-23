
namespace SeanProfileBlazor.Services
{
    public interface ITodoDataService
    {
        Task<bool> AddTodo(TodoModel todo);
        Task<bool> DeleteTodo(int id);
        Task<TodoModel> GetTodoById(int id);
        Task<IEnumerable<TodoModel>> GetTodos();
        Task<bool> UpdateTodo(TodoModel todo);
    }
}