namespace TodoList.Api.UnitTests;

public partial class TodoUnitTests
{
    [Fact]
    public async void Test_UpdateTodoItem_Updated()
    {
        await ClearDB();

        var item = new TodoItem { Id = System.Guid.NewGuid(), Description = "First Task", IsCompleted = false };
        Assert.Equal(TodoCreationResult.Created, await todoService.AddTodoItemAsync(item));

        item.Description = "First Task updated";
        Assert.Equal(TodoUpdateResult.Updated, await todoService.UpdateTodoItem(item));
    }

    [Fact]
    public async void Test_UpdateTodoItem_NotFound()
    {
        await ClearDB();

        var item = new TodoItem { Id = System.Guid.NewGuid(), Description = "First Task", IsCompleted = false };
        Assert.Equal(TodoCreationResult.Created, await todoService.AddTodoItemAsync(item));

        var item2 = new TodoItem { Id = System.Guid.NewGuid(), Description = "First Task", IsCompleted = false };
        Assert.Equal(TodoUpdateResult.NotFound, await todoService.UpdateTodoItem(item2));
    }

    [Fact]
    public async void Test_UpdateTodoItem_IsCompleted()
    {
        await ClearDB();

        var item = new TodoItem { Id = System.Guid.NewGuid(), Description = "First Task", IsCompleted = false };
        Assert.Equal(TodoCreationResult.Created, await todoService.AddTodoItemAsync(item));
        item.IsCompleted = true;
        Assert.Equal(TodoUpdateResult.Updated, await todoService.UpdateTodoItem(item));

        var singleItem = await todoService.GetTodoItem(item.Id);

        Assert.NotNull(singleItem);
        Assert.Equal(item.Id, singleItem.Id);
        Assert.Equal(item.Description, singleItem.Description);
        Assert.Equal(item.IsCompleted, singleItem.IsCompleted);
    }
}
