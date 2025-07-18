using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telemetry.Data.Dtos;
using Telemetry.Data.Models;
using Telemetry.Repositories.Interfaces;
namespace TelemetryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndustryController : ControllerBase
    {
        private readonly IIndustryRepository repository;
        public IndustryController(IIndustryRepository _repository)
        {
            repository = _repository;
        }
        [HttpPost]
        [Route("createIndustry")]
        public IActionResult Create([FromBody] IndustryDto dto)
        {            
            repository.create(dto);
            return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
        }

        [HttpPut]
        [Route("updateIndustry")]
        public IActionResult Update([FromBody] IndustryDto dto)
        {
            var existing = repository.get(dto.Id);
            if (existing == null)
                return NotFound();
            
            repository.update(dto);

            return NoContent();
        }

        [HttpDelete]
        [Route("deleteIndustry")]
        public IActionResult Delete(int id)
        {
            var existing = repository.get(id);
            if (existing == null)
                return NotFound();

            repository.delete(id);
            return NoContent();
        }

        [HttpGet]
        [Route("getallIndustry")]
        public IActionResult GetAll()
        {
            List<Industry> industries=repository.getAll();
            return Ok(industries);
        }

        [HttpGet]
        [Route("getIndustry")]
        public IActionResult Get(int id)
        {
            var industry = repository.get(id);
            if (industry == null)
                return NotFound();
            return Ok(industry);

        }
    }
}
