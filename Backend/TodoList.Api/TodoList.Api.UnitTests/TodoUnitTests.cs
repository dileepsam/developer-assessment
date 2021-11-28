global using System.Threading.Tasks;

global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Logging;

global using TodoList.Api.Models;

global using Xunit;

namespace TodoList.Api.UnitTests;

public partial class TodoUnitTests
{
    TodoService todoService;
    TodoContext todoContext;

    public TodoUnitTests()
    {
        todoContext = new TodoContext(new DbContextOptionsBuilder<TodoContext>()
                                        .UseInMemoryDatabase(databaseName: "TodoDataBase").Options);

        todoService = new TodoService(todoContext, LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<ITodoService>());
    }

    private async Task ClearDB()
    {
        todoContext.RemoveRange(todoContext.TodoItems);
        await todoContext.SaveChangesAsync();
    }
}
