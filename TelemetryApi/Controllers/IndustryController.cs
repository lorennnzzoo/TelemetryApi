using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TelemetryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndustryController : ControllerBase
    {
        [HttpPost]
        [Route("createIndustry")]
        public IActionResult Create()
        {
            return Ok();
        }

        [HttpPut]
        [Route("updateIndustry")]
        public IActionResult Update()
        {
            return Ok();
        }

        [HttpDelete]
        [Route("deleteIndustry")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }

        [HttpGet]
        [Route("getallIndustry")]
        public IActionResult GetAll()
        {
            return Ok();
        }

        [HttpGet]
        [Route("getIndustry")]
        public IActionResult Get(int id)
        {
            return Ok();
        }
    }
}
