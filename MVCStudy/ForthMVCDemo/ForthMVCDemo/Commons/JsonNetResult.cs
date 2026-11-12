using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;
using System.Web.Mvc;

namespace ForthMVCDemo.Commons
{
    /// <summary>
    /// 统一 JSON 输出：属性名转 camelCase，DateTime 走 ISO 8601
    /// </summary>
    public class JsonNetResult : ActionResult
    {
        public object Data { get; set; }

        public JsonNetResult(object data)
        {
            Data = data;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = "application/json";
            response.ContentEncoding = Encoding.UTF8;

            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateFormatHandling = DateFormatHandling.IsoDateFormat
            };

            response.Write(JsonConvert.SerializeObject(Data, settings));
        }
    }
}
