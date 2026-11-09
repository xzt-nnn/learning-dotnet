using SecondWebFormApplication.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SecondWebFormApplication
{
    /// <summary>
    /// CityService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
     [System.Web.Script.Services.ScriptService]
    public class CityService : System.Web.Services.WebService
    {
        private CityBLL bll = new CityBLL();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public List<Dictionary<string, object>> GetCityList()
        {
            int total;
            DataTable dt = bll.GetCityPage(0, 10, out total);

            // DataTable 不能被 JavaScriptSerializer 直接序列化，
            // 先转成 List<Dictionary>，序列化后就是标准的 JSON 数组
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (DataRow row in dt.Rows)
            {
                Dictionary<string, object> item = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    item[col.ColumnName] = row[col];
                }
                list.Add(item);
            }
            return list;
        }

        [WebMethod]
        public string GetCityNameById(int id)
        {
            return "测试城市";
        }
    }
}
