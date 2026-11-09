using Microsoft.AspNetCore.Mvc;

namespace Net8TodoControllerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DependencyController : ControllerBase
{
    private readonly IMyDependency _single;           // 解析单个：取最后一个注册的
    private readonly IEnumerable<IMyDependency> _all;  // 解析全部：按注册顺序

    public DependencyController(
        //获取的是最后一次注入的依赖
        IMyDependency single,
        //获取全部
        IEnumerable<IMyDependency> all)
    {
        _single = single;
        _all = all;
    }

    [HttpGet("single")]
    public IActionResult GetSingle() => Ok(_single.GetName());

    [HttpGet("all")]
    public IActionResult GetAll() => Ok(_all.Select(d => d.GetName()));
}
