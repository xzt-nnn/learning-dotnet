using Ef6MvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ef6MvcDemo.Repository
{
    public interface IUserRepository
    {
        Task<List<User>> userList();
        Task<User> GetById(int id);

        Task<User> AddUser(User user);

        //用户名是否已存在：仓储只提供查询，是否允许新增由 Service 判断
        Task<bool> ExistsByUserNameAsync(string userName);

        Task<User> Edit(User user);

        Task DeleteUser(User user);

        Task<PagedResult<User>> UserListPagedAsync(int pageIndex, int pageSize, string keyword);

        //批量删除
        Task<int> BatchDeleteAsync(List<int> ids);

        Task<bool> UpdateAvatarAsync(int id, string avatarPath);

    }
}
