namespace Net8TodoControllerApi;

public class TodoService : ITodoService
{
    private readonly List<Todo> _data = new()
    {
        new Todo { Id = 1, Title = "学习ASP.NET Core控制器", IsDone = false }
    };
    private int _nextId = 2;

    public List<Todo> GetAll() => _data;

    public Todo? GetById(int id) => _data.FirstOrDefault(t => t.Id == id);

    public Todo Add(TodoDto dto)
    {
        var todo = new Todo { Id = _nextId++, Title = dto.Title, IsDone = dto.IsDone };
        _data.Add(todo);
        return todo;
    }

    public bool Update(int id, TodoDto dto)
    {
        var todo = GetById(id);
        if (todo is null) return false;
        todo.Title = dto.Title;
        todo.IsDone = dto.IsDone;
        return true;
    }

    public bool Delete(int id)
    {
        var todo = GetById(id);
        return todo is not null && _data.Remove(todo);
    }
}
