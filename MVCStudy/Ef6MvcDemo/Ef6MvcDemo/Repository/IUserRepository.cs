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
        Task<List<User>> userList();
        Task<User> GetById(int id);

        Task<User> AddUser(User user);

        Task<User> Edit(User user);

        Task DeleteUser(User user);
    }
}
