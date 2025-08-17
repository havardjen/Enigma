using EnigmaComponents.Classes;
using EnigmaResources.Model;
using EnigmaResources.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EnigmaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodingController : ControllerBase
    {
        private MainWindowViewModel _vm;

        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok("Welcome to the Enigma API!");
        }

        [HttpPost, Route("CodeDecode")]
        public ActionResult<string> CodeDecode([FromBody]List<RotorSettings> codingSettings, string message)
        {
            _vm = new MainWindowViewModel(new Steckerbrett());
            
            SetWalzenLage(codingSettings);
            SetRingStellungen(codingSettings);
            SetPositions(codingSettings);

            _vm.OriginalMessage = message;
            _vm.CodeDecodeMessage();

            return Ok(_vm.CodedDecodedMessage);
        }

        private void SetPositions(List<RotorSettings> codingSettings)
        {
            string positions = string.Empty;
            foreach (var s in codingSettings)
                positions += s.Position;

            _vm.SetPositions(positions);
        }

        private void SetRingStellungen(List<RotorSettings> codingSettings)
        {
            string ringStellungen = string.Empty;

            foreach (var s in codingSettings)
                ringStellungen += s.Ringstellung;

            _vm.SetRingStellungen(ringStellungen);
        }

        private void SetWalzenLage(List<RotorSettings> codingSettings)
        {   
            var walzenLage = new List<string>();

            foreach (var s in codingSettings)
                walzenLage.Add(s.RotorName);

            _vm.SetWalzenlage(walzenLage);
        }
    }
}
