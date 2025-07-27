using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnigmaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodingController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok("Welcome to the Enigma API!");
        }

        [HttpGet, Route("CodeDecode")]
        public ActionResult<string> CodeDecode()
        {
            return Ok("TEST");
        }
    }
}
