using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Scanfer.Studio.DockerTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("Index")]
        public string Index()
        {
            return $"这是一个久违的接口.{DateTime.Now.ToString("yyyyMMddHHmmssfff")}";
        }
    }
}
