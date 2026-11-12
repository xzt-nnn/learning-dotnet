using Ef6MvcDemo.Models;
using Ef6MvcDemo.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

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

        //========== 业务规则上移：用户名唯一判断从仓储层搬到 Service ==========
        public async Task<int> AddUser(User user)
        {
            //业务规则：用户名已存在则不新增
            if (await _userRepo.ExistsByUserNameAsync(user.UserName))
            {
                return 0;
            }

            await _userRepo.AddUser(user);
            return 1;
        }

        public async Task<int> BatchDeleteAsync(List<int> ids)
        {
            if (ids == null || ids.Count == 0) return 0;
            return await _userRepo.BatchDeleteAsync(ids);
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

        public async Task<string> UploadAvatarAsync(int id, Stream stream, string originalFileName, 
            long contentLength, string saveDirectory)
        {
            //1.参数校验
            if (contentLength <= 0)
            {
                throw new ArgumentException("请选择要上传的图片");
            }
            if (contentLength > 2 * 1024 * 1024)
            {
                throw new ArgumentException("图片不能超过2MB");
            }
            //拿到文件的后缀名
            string ext = Path.GetExtension(originalFileName).ToLower();
            string[] allowExt = { ".jpg", ".jpeg", ".png", ".gif" };

            if (!allowExt.Contains(ext))
            {
                throw new ArgumentException("只支持jpg/png/gif格式");
            }
            var user = await _userRepo.GetById(id);
            if (user == null) throw new ArgumentException("该用户不存在");

            //先把旧路径存下来：user 是 EF 跟踪的实体，
            //UpdateAvatarAsync 之后它的 AvatarPath 会变成新路径，再取就晚了
            string oldAvatarPath = user.AvatarPath;

            //2.确保目录存在
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            //3.
            string fileName = Guid.NewGuid().ToString("N") + ext;
            string fullPath = Path.Combine(saveDirectory, fileName);

            using (var fs=new FileStream(fullPath, FileMode.Create))
            {
                await stream.CopyToAsync(fs);
            }

            //4.更新数据库
            string relativePath = "/Uploads/Avatar/" + fileName;
            await _userRepo.UpdateAvatarAsync(id, relativePath);

            //5.删除旧头像（用之前保存的 oldAvatarPath，而不是被改过的 user.AvatarPath）
            if (!string.IsNullOrEmpty(oldAvatarPath))
            {
                string oldFile = Path.Combine(saveDirectory, Path.GetFileName(oldAvatarPath));
                if (File.Exists(oldFile))
                {
                    File.Delete(oldFile);
                }
            }
            return relativePath;
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

        //分页查询：统计总数和取当前页都在仓储层完成，这里只做透传
        public async Task<PagedResult<User>> UserListPagedAsync(int pageIndex, int pageSize, string keyword)
        {
            return await _userRepo.UserListPagedAsync(pageIndex, pageSize, keyword);
        }


    }
}
