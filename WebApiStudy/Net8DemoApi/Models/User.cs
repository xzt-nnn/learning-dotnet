namespace Net8DemoApi.Models;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateTime CreateTime { get; set; }
}
