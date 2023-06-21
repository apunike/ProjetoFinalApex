using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Feito Get";
        }

        [HttpPost]
        public string Post()
        {
            return "Feito Post";
        }

        [HttpPut]
        public string Put()
        {
            return "feito Put";
        }

        [HttpDelete]
        public string Delete()
        {
            return "feito Delete";
        }
    }
}
