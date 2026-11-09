using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Ef6MvcDemo.Models;
using Ef6MvcDemo.Repository;
using Ef6MvcDemo.Service;

namespace Ef6MvcDemo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Autofac 依赖注入：统一在这里注册接口和实现
            var builder = new ContainerBuilder();

            //每个请求共享同一个 DbContext
            builder.RegisterType<MyDbContext>().AsSelf().InstancePerRequest();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerRequest();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerRequest();

            //注册所有控制器，控制器构造函数里的参数由容器自动注入
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            var container = builder.Build();
            //把 MVC 的依赖解析器换成 Autofac
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
