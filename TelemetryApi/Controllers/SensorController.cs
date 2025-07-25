﻿using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telemetry.Data.Dtos;
using Telemetry.Data.Models;
using Telemetry.Repositories.Interfaces;
using Sensor = Telemetry.Data.Models.Sensor;

namespace TelemetryApi.Controllers
{
    //[Authorize(Roles = "PcbAdmin")]
    [Route("api/[controller]")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly ISensorRepository repository;
        public SensorController(ISensorRepository _repository)
        {
            repository = _repository;
        }
        [HttpPost]
        [Route("createSensor")]
        public IActionResult Create(SensorDto dto)
        {
            repository.create(dto);
            return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
        }

        [HttpPut]
        [Route("updateSensor")]
        public IActionResult Update(SensorDto dto)
        {
            var existing = repository.get(dto.Id);
            if (existing == null)
                return NotFound();

            repository.update(dto);

            return NoContent();
        }

        [HttpDelete]
        [Route("deleteSensor")]
        public IActionResult Delete(int id)
        {
            try
            {
                repository.delete(id);
                return Ok("Sensor deletion successfull.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getallSensorByStation")]
        public IActionResult GetAllByStation(int id)
        {
            List<Sensor> sensors = repository.getAll().Where(e => e.StationId == id).ToList();
            return Ok(sensors);
        }

        [HttpGet]
        [Route("getSensor")]
        public IActionResult Get(int id)
        {
            var sensor = repository.get(id);
            if (sensor == null)
                return NotFound();
            return Ok(sensor);
        }
    }
}
