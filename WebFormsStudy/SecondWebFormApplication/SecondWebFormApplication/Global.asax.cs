using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;

namespace SecondWebFormApplication
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // 注册 Web API 路由（api/{controller}/{id}）
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}