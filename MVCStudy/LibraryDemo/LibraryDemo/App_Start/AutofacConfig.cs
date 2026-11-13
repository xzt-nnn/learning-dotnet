using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using LibraryDemo.Models;
using LibraryDemo.Services;

namespace LibraryDemo.App_Start
{
    public class AutofacConfig
    {

        public static IContainer Container { get; private set; }

        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            //1.注册程序中的所有Controller，交给容器创建
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            //2.DbContext:每个Http请求一个实例
            builder.RegisterType<LibraryContext>()
                .AsSelf()//这个就是用本命注册，此时取也要用本名取
                .InstancePerRequest();

            //3.注入业务服务
            builder.RegisterType<BookService>()
                .As<IBookService>()
                .InstancePerRequest();

            builder.RegisterType<CategoryService>()
                .As<ICategoryService>()
                .InstancePerRequest();

            //4.过滤器
            builder.RegisterFilterProvider();

            //5.创建容器
            Container = builder.Build();

            //把容器给mvc
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }


    }
}