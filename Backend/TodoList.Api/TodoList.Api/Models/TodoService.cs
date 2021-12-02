namespace TodoList.Api.Models;

/// <summary>
/// TodoService 
/// Separating this logic from the Controller 
/// it helps to have this separate from controller making Controller to handle only rest related logic.
/// helps with reuse of this service like if migrated/used along with grpc SOAP or even desktop app
/// </summary>
public class TodoService : ITodoService
{
    private readonly TodoContext _todoContext;
    private readonly ILogger<ITodoService> _logger;

    public TodoService(TodoContext todoContext, ILogger<ITodoService> logger)
    {
        _todoContext = todoContext;
        _logger = logger;
    }

    public async Task<List<TodoItem>> GetTodoItems() => await _todoContext.TodoItems.ToListAsync();

    public async Task<TodoItem> GetTodoItem(Guid id) => await _todoContext.TodoItems.FindAsync(id);

    public async Task<TodoCreationResult> AddTodoItemAsync(TodoItem todoItem)
    {
        if (TodoItemDescriptionExists(todoItem.Description))
            return TodoCreationResult.AlreadyExist;

        _todoContext.TodoItems.Add(todoItem);

        try
        {
            if (await _todoContext.SaveChangesAsync() == 1)
                return TodoCreationResult.Created;
            else
                return TodoCreationResult.Error;
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Something went wrong while creating a TodoItem", todoItem);
        }
        return TodoCreationResult.Error;
    }

    public async Task<TodoUpdateResult> UpdateTodoItem(TodoItem todoItem)
    {
        _todoContext.Entry(todoItem).State = EntityState.Modified;

        if (!TodoItemIdExists(todoItem.Id))
            return TodoUpdateResult.NotFound;

        try
        {
            await _todoContext.SaveChangesAsync();
            return TodoUpdateResult.Updated;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogCritical(ex, "Something went wrong while updating a TodoItem", todoItem);
        }

        return TodoUpdateResult.Error;
    }

    private bool TodoItemIdExists(Guid id)
    {
        return _todoContext.TodoItems.Any(x => x.Id == id);
    }

    private bool TodoItemDescriptionExists(string description)
    {
        return _todoContext.TodoItems
               .Any(x => x.Description.ToUpperInvariant() == description.ToUpperInvariant() && !x.IsCompleted);
    }
}
