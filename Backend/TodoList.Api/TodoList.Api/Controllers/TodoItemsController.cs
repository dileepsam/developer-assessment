namespace TodoList.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoItemsController : ControllerBase
{
    private readonly ILogger<TodoItemsController> _logger;
    private readonly ITodoService _todoService;

    public TodoItemsController(ILogger<TodoItemsController> logger, ITodoService todoService)
    {
        _logger = logger;
        _todoService = todoService;
    }

    // GET: api/TodoItems
    [HttpGet]
    public async Task<IActionResult> GetTodoItems() => Ok(await _todoService.GetTodoItems());

    // GET: api/TodoItems/...
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTodoItem(Guid id)
    {
        TodoItem result = await _todoService.GetTodoItem(id);
        return result == null ? NotFound() : Ok(result);
    }

    // PUT: api/TodoItems/... 
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTodoItem(Guid id, TodoItem todoItem)
    {
        if (id != todoItem.Id)
            return BadRequest();

        TodoUpdateResult result = await _todoService.UpdateTodoItem(todoItem);

        if (result == TodoUpdateResult.Updated)
            return Ok();

        if (result == TodoUpdateResult.NotFound)
            return NotFound("Todo item not found");

        // in future if anymore failure cases introduced this will work as a generic error message.
        _logger.LogError("Something went wrong while updating item.", result);
        return BadRequest($"Something went wrong: {result}");
    }

    // POST: api/TodoItems 
    [HttpPost]
    public async Task<IActionResult> PostTodoItem(TodoItem todoItem)
    {
        if (string.IsNullOrEmpty(todoItem?.Description))
            return BadRequest("Description is required");

        TodoCreationResult result = await _todoService.AddTodoItemAsync(todoItem);

        if (result == TodoCreationResult.AlreadyExist)
            return BadRequest("Description already exists");

        if (result == TodoCreationResult.Created)
            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);

        // in future if anymore failure cases introduced this will work as a generic error message.
        return BadRequest($"Something went wrong: {result}");
    }
}
