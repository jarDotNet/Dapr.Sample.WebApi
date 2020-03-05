using Microsoft.AspNetCore.Mvc;
using System;

namespace Dapr.Sample.WebApi.Controllers
{
    [ApiController]
    public class ServiceInvocationController : ControllerBase
    {
        [Topic("hello")]
        [HttpGet("hello")]
        public ActionResult<string> Get()
        {
            Console.WriteLine("Hello, World.");
            return "World";
        }
    }
}
