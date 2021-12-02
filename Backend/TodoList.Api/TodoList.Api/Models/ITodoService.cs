namespace TodoList.Api.Models;

public interface ITodoService
{
    Task<TodoCreationResult> AddTodoItemAsync(TodoItem todoItem);
    Task<TodoItem> GetTodoItem(Guid id);
    Task<List<TodoItem>> GetTodoItems();
    Task<TodoUpdateResult> UpdateTodoItem(TodoItem todoItem);
}
