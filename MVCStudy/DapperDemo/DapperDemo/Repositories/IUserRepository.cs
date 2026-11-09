using DapperDemo.Models;
using System.Collections.Generic;

public interface IUserRepository
{
    List<UserInfo> GetAll();
    int Add(UserInfo user);
}
