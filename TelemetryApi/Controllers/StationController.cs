using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TelemetryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationController : ControllerBase
    {
        [HttpPost]
        [Route("createStation")]
        public IActionResult Create()
        {
            return Ok();
        }

        [HttpPut]
        [Route("updateStation")]
        public IActionResult Update()
        {
            return Ok();
        }

        [HttpDelete]
        [Route("deleteStation")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }

        [HttpGet]
        [Route("getallStation")]
        public IActionResult GetAll()
        {
            return Ok();
        }

        [HttpGet]
        [Route("getStation")]
        public IActionResult Get(int id)
        {
            return Ok();
        }
    }
}
