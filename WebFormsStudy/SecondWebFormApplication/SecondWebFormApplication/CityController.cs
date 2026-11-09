using System.Web.Http;

namespace SecondWebFormApplication
{
    // 统一前缀，下面所有[Route]都是拼接在这个后面
    [RoutePrefix("api/city")]
    public class CityController : ApiController
    {
        // 拼接后：api/city/getlist
        [HttpGet]
        [Route("getlist")]
        public IHttpActionResult GetCityList()
        {
            return Ok("城市列表");
        }

        // 拼接后：api/city/{id}/info
        [HttpGet]
        [Route("{id}/info")]
        public IHttpActionResult GetCityInfo(int id)
        {
            return Ok($"城市id={id}");
        }

        // 【特殊】如果某个接口不想用RoutePrefix前缀，加 [Route("~/xxx")]，~代表根路径
        [HttpGet]
        [Route("~/api/allcity")]
        public IHttpActionResult AllCity()
        {
            return Ok("全部城市");
        }

        [HttpPost]
        [Route("add")]
        public IHttpActionResult AddCity([FromBody] CityModel model)
        {
            //第一步：校验数据
            if (model == null)
            {
                return BadRequest("提交的数据不能为空");
            }
            if (string.IsNullOrEmpty(model.Province) || string.IsNullOrEmpty(model.Name))
            {
                return BadRequest("省份和城市不能为空");
            }

            return Ok(new
            {
                code = 200,
                msg = "新增成功",
                data = model
            });
        }

        [HttpGet]
        [Route("testerror")]
        public IHttpActionResult TestError()
        {
            //手动抛出异常
            int a = 0;
            int b = 10 / a;
            return Ok(a);
        }



    }
}
