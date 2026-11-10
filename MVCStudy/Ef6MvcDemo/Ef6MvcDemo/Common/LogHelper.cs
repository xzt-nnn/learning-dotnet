using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ef6MvcDemo.Common
{
    public class LogHelper
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LogHelper));

        public static void Info(string msg) => log.Info(msg);
        public static void Error(string msg, Exception ex = null) => log.Error(msg, ex);
        public static void Debug(string msg) => log.Debug(msg);
    }
}