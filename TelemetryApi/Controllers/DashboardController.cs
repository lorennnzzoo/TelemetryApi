using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telemetry.Repositories.Interfaces;

namespace TelemetryApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardRepository repository;
        public DashboardController(IDashboardRepository _repository)
        {
            repository = _repository;
        }
        [HttpGet]
        [Route("GetIndustryDashboard/{industryId}")]
        public IActionResult GetIndustryDashboard(int industryId)
        {
            return Ok(repository.GetIndustryDashboard(industryId));
        }
    }
}
