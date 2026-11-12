using Microsoft.AspNetCore.Mvc;
using Net8DemoApi.Models;

namespace Net8DemoApi.Controllers;

[ApiController]//HttpPost默认只接受json格式的参数
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    // 临时内存模拟数据库，重启数据就丢失
    private static readonly List<User> _users = new()
    {
        new User{Id = 1, UserName = "张三", Phone = "13800138000", CreateTime = DateTime.Now}
    };

    /// <summary>
    /// 查询全部用户 GET /api/user
    /// </summary>
    [HttpGet]
    public ActionResult<List<User>> GetAll()
    {
        return Ok(_users);
    }

    /// <summary>
    /// 根据id查询单个用户 GET /api/user/1
    /// </summary>
    [HttpGet("{id}")]
    public ActionResult<User> GetById(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound("用户不存在");
        }
        return Ok(user);
    }

    /// <summary>
    /// 新增用户 POST /api/user
    /// </summary>
    [HttpPost]
    public ActionResult<User> Add(User user)
    {
        user.Id = _users.Max(u => u.Id) + 1;
        user.CreateTime = DateTime.Now;
        _users.Add(user);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    /// <summary>
    /// 修改用户 PUT /api/user/1
    /// </summary>
    [HttpPut("{id}")]
    public IActionResult Update(int id, User user)
    {
        if (id != user.Id) return BadRequest();
        var dbUser = _users.FirstOrDefault(u => u.Id == id);
        if (dbUser == null) return NotFound();

        dbUser.UserName = user.UserName;
        dbUser.Phone = user.Phone;
        return NoContent();
    }

    /// <summary>
    /// 删除用户 DELETE /api/user/1
    /// </summary>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null) return NotFound();
        _users.Remove(user);
        return NoContent();
    }
}
