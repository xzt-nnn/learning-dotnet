using Microsoft.AspNetCore.Mvc;

namespace Net8TodoControllerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HelloController : ControllerBase
{
    //[FromService]形式的依赖注入
    [HttpGet]
    public IActionResult sayHello([FromServices] IHelloService helloService)
    {
        return Ok(helloService.sayHello());
    }
}
