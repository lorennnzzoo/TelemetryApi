using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TelemtryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        [HttpPost]
        [Route("createCompany")]
        public IActionResult Create()
        {
            return Ok();
        }

        [HttpPut]
        [Route("updateCompany")]
        public IActionResult Update()
        {
            return Ok();
        }

        [HttpDelete]
        [Route("deleteCompany")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }

        [HttpGet]
        [Route("getallCompanies")]
        public IActionResult GetAll()
        {
            return Ok();
        }

        [HttpGet]
        [Route("getCompany")]
        public IActionResult Get(int id)
        {
            return Ok();
        }
    }
}
