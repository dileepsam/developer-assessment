namespace TodoList.Api.UnitTests;

public partial class TodoUnitTests
{
    [Fact]
    public async void Test_GetTodoItems()
    {
        await ClearDB();
        Assert.Empty(todoContext.TodoItems);

        var item = new TodoItem { Id = System.Guid.NewGuid(), Description = "First Task", IsCompleted = false };
        await todoService.AddTodoItemAsync(item);
        var item2 = new TodoItem { Id = System.Guid.NewGuid(), Description = "Second Task", IsCompleted = false };
        await todoService.AddTodoItemAsync(item2);

        var items = await todoService.GetTodoItems();

        Assert.Equal(2, items.Count);

        Assert.Equal(item.Id, items[0].Id);
        Assert.Equal(item.Description, items[0].Description);
        Assert.Equal(item.IsCompleted, items[0].IsCompleted);

        Assert.Equal(item2.Id, items[1].Id);
        Assert.Equal(item2.Description, items[1].Description);
        Assert.Equal(item2.IsCompleted, items[1].IsCompleted);
    }

    [Fact]
    public async void Test_GetTodoItems_Default_Empty()
    {
        await ClearDB();
        Assert.Empty(todoContext.TodoItems);

        var items = await todoService.GetTodoItems();
        Assert.Empty(items);
    }
}
