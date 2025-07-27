using EnigmaResources.Model;
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

        [HttpPost, Route("CodeDecode")]
        public ActionResult<string> CodeDecode([FromBody]EnigmaSettingsDto codingSettings)
        {
            var rotors = codingSettings.Rotors.Select(r =>
                new RotorSettings(RotorFactory.Create(r.RotorType, r.RotorNumber),
                                  r.Ringstellung,
                                  r.Position)
            ).ToArray();

            var tmpResults = $"Message: {codingSettings.Message}. Rotor I: {(rotors[0].Rotor.RotorName)}";
            return Ok(tmpResults);
        }
    }
}
