using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TelemetryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        [HttpPost]
        [Route("createSensor")]
        public IActionResult Create()
        {
            return Ok();
        }

        [HttpPut]
        [Route("updateSensor")]
        public IActionResult Update()
        {
            return Ok();
        }

        [HttpDelete]
        [Route("deleteSensor")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }

        [HttpGet]
        [Route("getallSensor")]
        public IActionResult GetAll()
        {
            return Ok();
        }

        [HttpGet]
        [Route("getSensor")]
        public IActionResult Get(int id)
        {
            return Ok();
        }
    }
}
