using Microsoft.AspNetCore.Mvc;

namespace Net8TodoControllerApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly ITodoService todoService;

    //构造函数注入
    public TodoController(ITodoService todoService)
    {
        this.todoService = todoService;
    }

    [HttpGet]
    public ActionResult<List<Todo>> GetAll()
    {
        return Ok(todoService.GetAll());
    }
}
