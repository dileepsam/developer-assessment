namespace TodoList.Api.UnitTests;

public partial class TodoUnitTests
{
    [Fact]
    public async void Test_AddTodoItemAsync_OneItem()
    {
        await ClearDB();
        Assert.Empty(todoContext.TodoItems);

        var item = new TodoItem { Id = System.Guid.NewGuid(), Description = "First Task", IsCompleted = false };
        await todoService.AddTodoItemAsync(item);
        var items = await todoService.GetTodoItems();

        Assert.Single(items);

        var returnedItem = items[0];
        Assert.Equal(item.Id, returnedItem.Id);
        Assert.Equal(item.Description, returnedItem.Description);
        Assert.Equal(item.IsCompleted, returnedItem.IsCompleted);

        var singleItem = await todoService.GetTodoItem(item.Id);
        Assert.Equal(item.Id, singleItem.Id);
        Assert.Equal(item.Description, singleItem.Description);
        Assert.Equal(item.IsCompleted, singleItem.IsCompleted);
    }

    [Fact]
    public async void Test_AddTodoItemAsync_DuplicateDescription_Fails()
    {
        await ClearDB();

        var item = new TodoItem { Id = System.Guid.NewGuid(), Description = "First Task", IsCompleted = false };
        var item2 = new TodoItem { Id = System.Guid.NewGuid(), Description = "First Task", IsCompleted = false };

        Assert.Equal(TodoCreationResult.Created, await todoService.AddTodoItemAsync(item));
        Assert.Equal(TodoCreationResult.AlreadyExist, await todoService.AddTodoItemAsync(item2));
    }

    [Fact]
    public async void Test_AddTodoItemAsync_DuplicateDescription_Pass_DiffrentCompletedStatus()
    {
        await ClearDB();

        var item = new TodoItem { Id = System.Guid.NewGuid(), Description = "First Task", IsCompleted = true };
        var item2 = new TodoItem { Id = System.Guid.NewGuid(), Description = "First Task", IsCompleted = false };

        Assert.Equal(TodoCreationResult.Created, await todoService.AddTodoItemAsync(item));
        Assert.Equal(TodoCreationResult.Created, await todoService.AddTodoItemAsync(item2));
    }

    [Fact]
    public async void Test_AddTodoItemAsync_Error_SameGUID()
    {
        await ClearDB();

        var item = new TodoItem { Id = System.Guid.NewGuid(), Description = "First Task", IsCompleted = true };
        Assert.Equal(TodoCreationResult.Created, await todoService.AddTodoItemAsync(item));

        item.Description = "Second Task";
        Assert.Equal(TodoCreationResult.Error, await todoService.AddTodoItemAsync(item));
    }
}
