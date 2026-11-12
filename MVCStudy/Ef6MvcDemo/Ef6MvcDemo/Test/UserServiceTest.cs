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

        //========== 业务规则上移后：唯一性判断改为 ExistsByUserNameAsync ==========
        //AddUser 新增成功：用户名不存在，Service 应返回 1
        [TestMethod]
        public async Task AddUser_新增成功_返回1()
        {
            //1.创建Mock仓库
            var mockRepo = new Mock<IUserRepository>();
            var user = new User { Id = 1, UserName = "test", Phone = "13800000000" };

            //2.设定：用户名不存在（改造前这里用 SetUp AddUser 返回对象来间接表达）
            mockRepo.Setup(x => x.ExistsByUserNameAsync(It.IsAny<string>())).ReturnsAsync(false);

            //3.构造 Service（只需要仓储）
            var service = new UserService(mockRepo.Object);

            //4.执行：测试方法本身也要 async/await
            int result = await service.AddUser(user);

            //5.断言：返回 1，且真正调用了入库
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

        //========== 业务规则上移后：唯一性判断改为 ExistsByUserNameAsync ==========
        //AddUser 用户名已存在：Service 直接返回 0，不应再入库
        [TestMethod]
        public async Task AddUser_用户名已存在_返回0()
        {
            var mockRepo = new Mock<IUserRepository>();
            var user = new User { UserName = "test" };

            //设置：用户名已存在（改造前用 AddUser 返回 null 来表达业务失败）
            mockRepo.Setup(x => x.ExistsByUserNameAsync(It.IsAny<string>())).ReturnsAsync(true);

            var service = new UserService(mockRepo.Object);

            int result = await service.AddUser(user);

            //断言：返回 0，且业务规则拦住后没有调用入库
            Assert.AreEqual(0, result);
            mockRepo.Verify(x => x.AddUser(It.IsAny<User>()), Times.Never);
        }
    }
}
