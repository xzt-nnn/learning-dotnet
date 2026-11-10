using Ef6MvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<User> AddUser(User user)
        {
            //用户名已存在则不新增
            if (_db.Users.Any(u => u.UserName == user.UserName))
            {
                return null;
            }

            _db.Users.Add(user);
            //真正提交到数据库，少了这句数据不会入库
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUser(User user)
        {
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
        }

        public async Task<User> Edit(User user)
        {
            _db.Entry(user).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetById(int id)
        {
           return await _db.Users.FindAsync(id);
        }

        public async Task<List<User>> userList()
        {
            return await _db.Users.AsNoTracking().ToListAsync();
        }
    }
}