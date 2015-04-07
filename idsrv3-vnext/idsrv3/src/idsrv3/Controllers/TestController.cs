using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace idsrv3.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class Test : Controller
    {
        [HttpGet]
        public JsonResult Get()
        {
            return Json(new
            {
                message = "You See this then it's ok auth is  :" + User.Identity.IsAuthenticated,
            });
        }
    }
}
