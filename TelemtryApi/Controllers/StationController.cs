using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TelemtryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationController : ControllerBase
    {
        [Route("create")]
        public IActionResult Create()
        {
            return Ok();
        }

        [Route("update")]
        public IActionResult Update()
        {
            return Ok();
        }

        [Route("delete")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }

        [Route("getall")]
        public IActionResult GetAll()
        {
            return Ok();
        }

        [Route("get")]
        public IActionResult Get(int id)
        {
            return Ok();
        }
    }
}
