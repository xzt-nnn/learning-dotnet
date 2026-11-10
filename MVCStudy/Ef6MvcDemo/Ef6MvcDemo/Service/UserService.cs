using Ef6MvcDemo.Models;
using Ef6MvcDemo.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Ef6MvcDemo.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        //========== 改造前（同步）：需要自己持有 DbContext 来 SaveChanges ==========
        //private readonly MyDbContext _db;
        //public UserService(IUserRepository userRepository, MyDbContext db)
        //{
        //    _userRepo = userRepository;
        //    _db = db;
        //}

        //========== 改造后（异步）：SaveChanges 已收敛到仓储层，不再需要 _db ==========
        public UserService(IUserRepository userRepository)
        {
            _userRepo = userRepository;
        }

        //========== 改造前（同步） ==========
        //public int AddUser(User user)
        //{
        //    User add = _userRepo.AddUser(user);
        //    if (add != null) return 1;
        //    else return 0;
        //}

        public async Task<int> AddUser(User user)
        {
            User add = await _userRepo.AddUser(user);
            if (add != null) return 1;
            else return 0;
        }

        //========== 改造前（同步） ==========
        //public bool DeleteUser(int id)
        //{
        //    User user = _userRepo.GetById(id);
        //    _userRepo.DeleteUser(user);
        //    _db.SaveChanges();
        //    return true;
        //}

        public async Task<bool> DeleteUser(int id)
        {
            User user = await _userRepo.GetById(id);
            //id 不存在时直接返回 false，避免把 null 传给仓储
            if (user == null) return false;

            await _userRepo.DeleteUser(user);
            return true;
        }

        //========== 改造前（同步） ==========
        //public bool EditUser(User user)
        //{
        //    _userRepo.Edit(user);
        //    _db.SaveChanges();
        //    return true;
        //}

        public async Task<bool> EditUser(User user)
        {
            await _userRepo.Edit(user);
            return true;
        }

        //========== 改造前（同步） ==========
        //public User GetUser(int id)
        //{
        //    return _userRepo.GetById(id);
        //}

        public async Task<User> GetUser(int id)
        {
            return await _userRepo.GetById(id);
        }

        //========== 改造前（同步） ==========
        //public List<User> userList()
        //{
        //    //throw new Exception("测试全局异常");
        //    return _userRepo.userList();
        //}

        public async Task<List<User>> userList()
        {
            //throw new Exception("测试全局异常");
            return await _userRepo.userList();
        }
    }
}
