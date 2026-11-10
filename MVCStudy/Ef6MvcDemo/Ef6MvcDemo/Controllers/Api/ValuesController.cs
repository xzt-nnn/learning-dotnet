using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Ef6MvcDemo.Controllers.Api
{
    public class ValuesController : ApiController
    {
        // GET: Values
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value" + id;
        }
    }
}