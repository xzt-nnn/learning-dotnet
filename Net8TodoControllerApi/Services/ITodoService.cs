namespace Net8TodoControllerApi;

public interface ITodoService
{
    List<Todo> GetAll();
    Todo? GetById(int id);
    Todo Add(TodoDto dto);
    bool Update(int id, TodoDto dto);
    bool Delete(int id);
}
