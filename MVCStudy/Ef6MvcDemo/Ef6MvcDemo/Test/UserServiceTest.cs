using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ef6MvcDemo.Models;
using Ef6MvcDemo.Repository;
using Ef6MvcDemo.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Ef6MvcDemo.Test
{
    [TestClass]
    public class UserServiceTest
    {
        //========== 改造前（同步）：Service 需要传 _db，Moq 用 Returns ==========
        ////AddUser 新增成功：仓库返回新增后的对象，Service 应返回 1
        //[TestMethod]
        //public void AddUser_新增成功_返回1()
        //{
        //    var mockRepo = new Mock<IUserRepository>();
        //    var user = new User { Id = 1, UserName = "test", Phone = "13800000000" };

        //    mockRepo.Setup(x => x.AddUser(It.IsAny<User>())).Returns(user);

        //    //AddUser 用不到 _db，这里传 null 即可
        //    var service = new UserService(mockRepo.Object, null);

        //    int result = service.AddUser(user);

        //    Assert.AreEqual(1, result);
        //    mockRepo.Verify(x => x.AddUser(user), Times.Once);
        //}

        //========== 改造后（异步）：Service 只依赖仓储，Moq 用 ReturnsAsync ==========
        //AddUser 新增成功：仓库返回新增后的对象，Service 应返回 1
        [TestMethod]
        public async Task AddUser_新增成功_返回1()
        {
            //1.创建Mock仓库
            var mockRepo = new Mock<IUserRepository>();
            var user = new User { Id = 1, UserName = "test", Phone = "13800000000" };

            //2.设定：异步方法用 ReturnsAsync 返回指定对象
            mockRepo.Setup(x => x.AddUser(It.IsAny<User>())).ReturnsAsync(user);

            //3.构造 Service（_db 依赖已去掉，只需要仓储）
            var service = new UserService(mockRepo.Object);

            //4.执行：测试方法本身也要 async/await
            int result = await service.AddUser(user);

            //5.断言
            Assert.AreEqual(1, result);
            mockRepo.Verify(x => x.AddUser(user), Times.Once);
        }

        //========== 改造前（同步） ==========
        ////AddUser 用户名已存在：仓库返回 null，Service 应返回 0
        //[TestMethod]
        //public void AddUser_用户名已存在_返回0()
        //{
        //    var mockRepo = new Mock<IUserRepository>();
        //    var user = new User { UserName = "test" };

        //    mockRepo.Setup(x => x.AddUser(It.IsAny<User>())).Returns((User)null);

        //    var service = new UserService(mockRepo.Object, null);

        //    int result = service.AddUser(user);

        //    Assert.AreEqual(0, result);
        //    mockRepo.Verify(x => x.AddUser(user), Times.Once);
        //}

        //========== 改造后（异步） ==========
        //AddUser 用户名已存在：仓库返回 null，Service 应返回 0
        [TestMethod]
        public async Task AddUser_用户名已存在_返回0()
        {
            var mockRepo = new Mock<IUserRepository>();
            var user = new User { UserName = "test" };

            //设置：返回 null 用 ReturnsAsync((User)null)，必须显式转类型
            mockRepo.Setup(x => x.AddUser(It.IsAny<User>())).ReturnsAsync((User)null);

            var service = new UserService(mockRepo.Object);

            int result = await service.AddUser(user);

            Assert.AreEqual(0, result);
            mockRepo.Verify(x => x.AddUser(user), Times.Once);
        }
    }
}
