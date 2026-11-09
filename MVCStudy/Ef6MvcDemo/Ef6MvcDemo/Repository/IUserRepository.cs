using Ef6MvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ef6MvcDemo.Repository
{
    public interface IUserRepository
    {
        List<User> userList();
        User GetById(int id);

        User AddUser(User user);

        User Edit(User user);

        void DeleteUser(User user);
    }
}
