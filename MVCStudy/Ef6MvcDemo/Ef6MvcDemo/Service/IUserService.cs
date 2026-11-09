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
        List<User> userList();
        User GetUser(int id);

        int AddUser(User user);

        bool EditUser(User user);

        bool DeleteUser(int id);
    }
}
