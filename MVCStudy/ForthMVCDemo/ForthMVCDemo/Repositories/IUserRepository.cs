using ForthMVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForthMVCDemo.Repositories
{
    public interface IUserRepository
    {
        bool UserNameExists(string userName);

        bool UserEmailExists(string email);
        void Add(User user);
    }
}
