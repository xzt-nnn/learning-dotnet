using ForthMVCDemo.Data;
using ForthMVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForthMVCDemo.Repositories
{
    public class UserRepository : IUserRepository
    {
        public void Add(User user)
        {
            using(var db=new UserDbContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public bool UserNameExists(string userName)
        {
            using (var db = new UserDbContext())
                return db.Users.Any(u => u.UserName == userName);
        }

        public bool UserEmailExists(string email)
        {
            using (var db = new UserDbContext())
                return db.Users.Any(u => u.Email == email);
        }
    }
}