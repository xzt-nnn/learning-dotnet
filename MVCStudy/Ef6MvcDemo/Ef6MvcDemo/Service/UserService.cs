using Ef6MvcDemo.Models;
using Ef6MvcDemo.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ef6MvcDemo.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly MyDbContext _db;
        public UserService(IUserRepository userRepository, MyDbContext db)
        {
            _userRepo = userRepository;
            _db = db;
        }

        public int AddUser(User user)
        {
            User add = _userRepo.AddUser(user);
            if (add != null) return 1;
            else return 0;
        }

        public bool DeleteUser(int id)
        {
            User user = _userRepo.GetById(id);
            _userRepo.DeleteUser(user);
            _db.SaveChanges();
            return true;
        }

        public bool EditUser(User user)
        {
            _userRepo.Edit(user);
            _db.SaveChanges();
            return true;
        }

        public User GetUser(int id)
        {
           return _userRepo.GetById(id);
           
        }

        public List<User> userList()
        {
            return _userRepo.userList();
        }
    }
}