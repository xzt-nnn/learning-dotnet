using Dapper;
using DapperDemo.Helpers;
using DapperDemo.Models;
using System.Collections.Generic;
using System.Linq;

namespace DapperDemo.Repository
{
    public class UserRepository : IUserRepository
    {
        public List<UserInfo> GetAll()
        {
            using (var conn = DbHelper.GetConnection())
            {
                string sql = "select * from UserInfo";
                var list = conn.Query<UserInfo>(sql).ToList();
                return list;
            }
        }

        public int Add(UserInfo user)
        {
            using (var conn = DbHelper.GetConnection())
            {
                string sql = @"INSERT INTO UserInfo(UserName,Age,Email)
                               VALUES(@UserName,@Age,@Email);
                               SELECT SCOPE_IDENTITY();";
                int newId = conn.ExecuteScalar<int>(sql, user);
                return newId;
            }
        }
    }
}
