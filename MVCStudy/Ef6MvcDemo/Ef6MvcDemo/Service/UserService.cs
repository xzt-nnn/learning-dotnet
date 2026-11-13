using Ef6MvcDemo.Models;
using Ef6MvcDemo.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

            //3.生成新的文件名
            string fileName = Guid.NewGuid().ToString("N") + ext;
            //获取保存图片的完整路径
            string fullPath = Path.Combine(saveDirectory, fileName);

            //保存代码文件
            //filestrean是文件流，用来本地磁盘文件的读写
            using (var fs=new FileStream(fullPath, FileMode.Create))
            {
                //异步将文件流写到文件里
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

        public async Task<byte[]> ExportUserCsvAsync(string keyword)
        {
            var list = await _userRepo.UserListForExportAsync(keyword);

            //MemoryStream内存流，数据全部写在服务器内存，不生成本地文件
            using(var ms = new MemoryStream())
            {
                //UTF8Encoding才会写入BOM，Excel识别中文的关键
                using(var writer = new StreamWriter(ms,new UTF8Encoding(true)))
                {
                    //写入csv表头（第一行），逗号分隔，对应表格列：Id,用户名、电话、创建时间、头像
                    await writer.WriteLineAsync("Id,用户名,电话,创建时间,头像");

                    foreach(var u in list)
                    {
                        await writer.WriteLineAsync(string.Join(",",
                            u.Id,
                            EscapeCsv(u.UserName),
                            EscapeCsv(u.Phone),
                            u.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                            EscapeCsv(u.AvatarPath)
                            ));
                    }
                }//dispose之后会flush，之后才能ToArray
                //当witer的using快结束，自动调用Dispose-》自动Flush，把缓冲区所有文字全部刷净MemoryStream
                return ms.ToArray();
            }

        }

        //CSV 转义：含逗号/引号/换行时用双引号包起来，内部引号翻倍
        private static string EscapeCsv(string value)
        {
            if (string.IsNullOrEmpty(value)) return "";

            //防 CSV 注入：以 = + - @ 开头的值会被 Excel 当公式执行
            if (value[0] == '=' || value[0] == '+' || value[0] == '-' || value[0] == '@')
            {
                value = "'" + value;
            }

            if (value.Contains(",") || value.Contains("\"") || value.Contains("\n") || value.Contains("\r"))
            {
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            }

            return value;
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

        public async Task<User> GetByUserNameAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName)) return null;
            return await _userRepo.GetByUserNameAsync(userName.Trim());
        }

        //校验通过返回用户，失败返回 null；密码比对属于业务规则，放在这里
        public async Task<User> ValidateUserAsync(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrEmpty(password))
                return null;

            var user = await _userRepo.GetByUserNameAsync(userName.Trim());

            if (user == null || string.IsNullOrEmpty(user.PasswordHash))
                return null;

            return System.Web.Helpers.Crypto.VerifyHashedPassword(user.PasswordHash, password)
                ? user
                : null;
        }


    }
}
