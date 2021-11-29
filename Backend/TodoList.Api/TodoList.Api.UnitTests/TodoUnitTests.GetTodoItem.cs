namespace TodoList.Api.UnitTests;

public partial class TodoUnitTests
{
    [Fact]
    public async void Test_GetTodoItem()
    {
        await ClearDB();

        var item = new TodoItem { Id = System.Guid.NewGuid(), Description = "First Task", IsCompleted = false };
        Assert.Equal(TodoCreationResult.Created, await todoService.AddTodoItemAsync(item));

        var singleItem = await todoService.GetTodoItem(item.Id);

        Assert.NotNull(singleItem);
        Assert.Equal(item.Id, singleItem.Id);
        Assert.Equal(item.Description, singleItem.Description);
        Assert.Equal(item.IsCompleted, singleItem.IsCompleted);
    }

    [Fact]
    public async void Test_GetTodoItem_NotExist()
    {
        await ClearDB();

        var guid = System.Guid.NewGuid();
        var singleItem = await todoService.GetTodoItem(guid);

        Assert.Null(singleItem);
    }
}
