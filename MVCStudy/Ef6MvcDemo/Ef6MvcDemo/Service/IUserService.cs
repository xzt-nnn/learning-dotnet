using Ef6MvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ef6MvcDemo.Service
{
    public interface IUserService
    {
        //========== 改造前（同步） ==========
        //List<User> userList();
        //User GetUser(int id);
        //int AddUser(User user);
        //bool EditUser(User user);
        //bool DeleteUser(int id);

        //========== 改造后（异步） ==========
        Task<List<User>> userList();
        Task<User> GetUser(int id);
        Task<int> AddUser(User user);
        Task<bool> EditUser(User user);
        Task<bool> DeleteUser(int id);
    }
}
