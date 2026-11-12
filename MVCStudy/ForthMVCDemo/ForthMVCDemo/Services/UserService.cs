using ForthMVCDemo.Commons;
using ForthMVCDemo.Models;
using ForthMVCDemo.Repositories;
using ForthMVCDemo.ViewModels;
using System;

namespace ForthMVCDemo.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public void Register(RegisterViewModel vm)
        {
            // 1. 唯一性校验，失败抛业务异常，由全局异常过滤器统一转成 JSON
            if (_userRepo.UserNameExists(vm.UserName))
                throw new BusinessException("该用户名已存在");

            if (_userRepo.UserEmailExists(vm.Email))
                throw new BusinessException("该邮箱已被注册");

            // 2. 生成盐并哈希密码，绝不存明文
            var salt = PasswordHasher.CreateSalt();

            // 3. 组装实体并入库
            _userRepo.Add(new User
            {
                UserName = vm.UserName,
                Email = vm.Email,
                NickName = vm.NickName,
                PasswordSalt = salt,
                PasswordHash = PasswordHasher.Hash(vm.Password, salt),
                Role = "User",
                IsActive = true,
                CreatedAt = DateTime.Now
            });
        }
    }
}
