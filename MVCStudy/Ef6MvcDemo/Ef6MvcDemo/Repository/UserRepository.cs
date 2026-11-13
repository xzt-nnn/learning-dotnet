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
            //========== 改造前：业务规则（用户名唯一）放在仓储层 ==========
            ////用户名已存在则不新增
            //if (_db.Users.Any(u => u.UserName == user.UserName))
            //{
            //    return null;
            //}

            //========== 改造后：仓储只负责入库，唯一性判断交给 Service ==========
            _db.Users.Add(user);
            //真正提交到数据库，少了这句数据不会入库
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<bool> ExistsByUserNameAsync(string userName)
        {
            return await _db.Users.AsNoTracking().AnyAsync(u => u.UserName == userName);
        }

        public async Task<int> BatchDeleteAsync(List<int> ids)
        {
            //没选中，就直接返回了
            if (ids == null || ids.Count == 0) return 0;

            //一次查询把要删除的记录一口气都查出来
            //Contains方法会被翻译为in()
            var users = await _db.Users.Where(u => ids.Contains(u.Id)).ToListAsync();
            if (users.Count == 0) return 0;

            _db.Users.RemoveRange(users);
            await _db.SaveChangesAsync();

            return users.Count;

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

        public async Task<PagedResult<User>> UserListPagedAsync(int pageIndex, int pageSize, string keyword)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize <= 0) pageSize = 10;

            //拼接查询条件
            IQueryable<User> query = _db.Users.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(u => u.UserName.Contains(keyword));
            }

            //统计总数
            int total = await query.CountAsync();

            int totalPages = (total + pageSize - 1) / pageSize;
            if(totalPages>0 && pageIndex > totalPages)
            {
                pageIndex = totalPages;
            }

            //取当前页，skip就是offset,take就是pageSize
            //select * from User order by id asc offset ... fetch next ...
            var items = await query.OrderBy(u => u.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<User>
            {
                Items = items,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = total,
                Keyword = keyword
            };

        }

        public async Task<bool> UpdateAvatarAsync(int id, string avatarPath)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return false;
            user.AvatarPath = avatarPath;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<User>> UserListForExportAsync(string keyword)
        {
            //关闭EF的实体追踪
            IQueryable<User> query = _db.Users.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(u => u.UserName.Contains(keyword));
            }
            //到处不加 Skip/Take,但要排序，保证每次到处顺序一致
            return await query.OrderBy(u => u.Id).ToListAsync();
        }

        //登录用：必须 Include(Role)，否则拿不到角色名，User.IsInRole 永远 false
        public async Task<User> GetByUserNameAsync(string userName)
        {
            return await _db.Users
                            .Include(u => u.Role)
                            .FirstOrDefaultAsync(u => u.UserName == userName);
        }
    }
}