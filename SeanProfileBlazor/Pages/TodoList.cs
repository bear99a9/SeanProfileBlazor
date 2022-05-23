using Microsoft.AspNetCore.Components;
using SeanProfileBlazor.Services;
using System.Globalization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace SeanProfileBlazor.Pages
{
    public partial class TodoList
    {
        public string TodaysDate { get; set; } = DateTime.Now.Date.ToShortDateString().ToString(new CultureInfo("en-GB"));

        public IEnumerable<TodoModel> Todos { get; set; } = new List<TodoModel>();

        public TodoModel Todo { get; set; }
        public DateTime Today { get; set; } = DateTime.Now;

        [Inject]
        public ITodoDataService todoDataService { get; set; }
        public int Count { get; set; } = 1;

        //used to store state of screen
        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;

        protected override async Task OnInitializedAsync()
        {
            Todo = new TodoModel() { DueDate = Today };
            Saved = false;

            try
            {
                Todos = await todoDataService.GetTodos();
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
        }

        protected async Task HandleValidTodoCreateSubmit()
        {
            Saved = false;

            if (Todo.Id == 0)
            {
                Todo.Status = "In progress";
                Saved = await todoDataService.AddTodo(Todo);
                if (Saved)
                {
                    StatusClass = "alert-success";
                    Message = "New todo added successfully.";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new todo. Please try again.";
                    Saved = false;
                }

                if (Saved)
                {
                    await OnInitializedAsync();
                }
            }
            
        }

        protected void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
        }

        protected async Task DeleteTodo(TodoModel todo)
        {
            Saved = await todoDataService.DeleteTodo(todo.Id);

            StatusClass = "alert-success";
            Message = "Deleted successfully";

            if (Saved)
            {
                await OnInitializedAsync();
            }

        }

        protected async Task UpdateTodo(TodoModel todo)
        {
            todo.IsCompleted = true;
            todo.Status = "Completed";

            Saved = await todoDataService.UpdateTodo(todo);

            StatusClass = "alert-success";
            Message = "Updated successfully";

            if (Saved)
            {
                await OnInitializedAsync();
            }

        }



    }
}
