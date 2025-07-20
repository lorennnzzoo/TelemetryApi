using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telemetry.Repositories.Interfaces;

namespace TelemetryApi.Controllers
{
    [RequireAuthKey]
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IKeyRepository repository;
        public UploadController(IKeyRepository _repository)
        {
            repository = _repository;
        }
        [HttpPost]
        [Route("liveUpload")]
        [RequireAuthKey]
        public IActionResult Live()
        {
            return Ok();
        }

        [HttpPost]
        [Route("delayUpload")]
        [RequireAuthKey]
        public IActionResult Delay()
        {
            return Ok();
        }
    }
}
