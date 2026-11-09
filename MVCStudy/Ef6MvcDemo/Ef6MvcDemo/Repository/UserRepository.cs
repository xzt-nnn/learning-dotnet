using Ef6MvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ef6MvcDemo.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDbContext _db;

        public UserRepository()
        {
        }

        public UserRepository(MyDbContext db)
        {
            _db = db;
        }

        public User AddUser(User user)
        {
            //用户名已存在则不新增
            if (_db.Users.Any(u => u.UserName == user.UserName))
            {
                return null;
            }

            _db.Users.Add(user);
            //真正提交到数据库，少了这句数据不会入库
            _db.SaveChanges();
            return user;
        }

        public void DeleteUser(User user)
        {
            _db.Users.Remove(user);
        }

        public User Edit(User user)
        {
            _db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            return user;
        }

        public User GetById(int id)
        {
           return _db.Users.Find(id);
        }

        public List<User> userList()
        {
            return _db.Users.ToList();
        }
    }
}