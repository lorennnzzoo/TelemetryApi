using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TelemetryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        [HttpPost]
        [Route("liveUpload")]
        public IActionResult Live()
        {
            return Ok();
        }

        [HttpPost]
        [Route("delayUpload")]
        public IActionResult Delay()
        {
            return Ok();
        }
    }
}
