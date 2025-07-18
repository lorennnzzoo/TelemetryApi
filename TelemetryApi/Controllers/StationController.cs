using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telemetry.Data.Dtos;
using Telemetry.Data.Models;
using Telemetry.Repositories.Interfaces;

namespace TelemetryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationController : ControllerBase
    {
        private readonly IStationRepository repository;
        public StationController(IStationRepository _repository)
        {
            repository = _repository;
        }
        [HttpPost]
        [Route("createStation")]
        public IActionResult Create(StationDto dto)
        {
            repository.create(dto);
            return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
        }

        [HttpPut]
        [Route("updateStation")]
        public IActionResult Update(StationDto dto)
        {
                var existing = repository.get(dto.Id);
                if (existing == null)
                    return NotFound();

                repository.update(dto);

                return NoContent();
        }

        [HttpDelete]
        [Route("deleteStation")]
        public IActionResult Delete(int id)
        {
            var existing = repository.get(id);
            if (existing == null)
                return NotFound();

            repository.delete(id);
            return NoContent();
        }

        [HttpGet]
        [Route("getallStationByIndustry")]
        public IActionResult GetAllByIndustry(int id)
        {
            List<Station> stations = repository.getAll().Where(e=>e.IndustryId==id).ToList();
            return Ok(stations);
        }

        [HttpGet]
        [Route("getStation")]
        public IActionResult Get(int id)
        {
            var station = repository.get(id);
            if (station == null)
                return NotFound();
            return Ok(station);
        }
    }
}
