using Autofac;
using Autofac.Integration.Mvc;
using SecondMVCDemo.BLL;
using SecondMVCDemo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SecondMVCDemo
{
    //这个类是整个网站的入口，**网站启动、关闭、每个请求到来都会触发对应的事件**。
    public class MvcApplication : System.Web.HttpApplication
    {
        
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //RouteTable：全局路由容器
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var builder = new ContainerBuilder();

            //注册所有Controller
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            //注册BLL接口与实例
            builder.RegisterType<CityBLL>().As<ICityBLL>().InstancePerRequest();

            //注册DAL
            builder.RegisterType<CityDAL>().InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}
