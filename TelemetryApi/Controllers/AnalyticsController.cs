using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telemetry.Repositories.Interfaces;

namespace TelemetryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsRepository repository;
        public AnalyticsController(IAnalyticsRepository _repository)
        {
            repository = _repository;
        }

        [HttpGet]
        [Route("getAnalytics")]
        public IActionResult GetAnalytics(
    [FromQuery] int industryId,
    [FromQuery] int[] stationIds,
    [FromQuery] int[] sensorIds,
    [FromQuery] DateTime from,
    [FromQuery] DateTime to,
      [FromQuery] int bucketMinutes)
        {
            try
            {
                var analytics = repository.GetAnalytics(industryId, stationIds, sensorIds, from.ToUniversalTime(), to.ToUniversalTime(),bucketMinutes);
                return Ok(analytics);
            }
            catch(ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
