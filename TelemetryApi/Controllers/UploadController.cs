using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Telemetry.Business;
using Telemetry.Data.Dtos;
using Telemetry.Repositories.Interfaces;

namespace TelemetryApi.Controllers
{
    [RequireAuthKey]
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IKeyRepository repository;
        private readonly IConfiguration configuration;
        public UploadController(IKeyRepository _repository, IConfiguration _configuration)
        {
            repository = _repository;
            configuration = _configuration;
        }
        [HttpPost]
        [Route("liveUpload")]
        [RequireAuthKey]
        public async Task<IActionResult> Live()
        {
            // 1. Check if PrivateKey is present
            if (!HttpContext.Items.TryGetValue("PrivateKey", out var privateKeyObj) || privateKeyObj is not string privateKey)
            {
                return Unauthorized("Private key missing.");
            }
            if (!HttpContext.Items.TryGetValue("AuthToken", out var authTokenObj) || authTokenObj is not string authToken)
            {
                return Unauthorized("Private key missing.");
            }

            // 2. Read body
            string encryptedPayload;
            using (var reader = new StreamReader(Request.Body))
            {
                encryptedPayload = await reader.ReadToEndAsync();
            }

            if (string.IsNullOrWhiteSpace(encryptedPayload))
            {
                return BadRequest("Request body is empty.");
            }

            string jsonPayload;
            try
            {
                jsonPayload = Rsa.DecryptPayload(encryptedPayload, privateKey);
            }
            catch (Exception ex)
            {
                return BadRequest($"Payload decryption failed");
            }

            StationUploadModel? model;
            try
            {
                model = JsonSerializer.Deserialize<StationUploadModel>(jsonPayload, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (JsonException)
            {
                return BadRequest("Invalid JSON format.");
            }
            if (model == null)
            {
                return BadRequest("Payload is null.");
            }

            if (model.StationId == null)
                return BadRequest("Station Id Missing.");
            if (model.StationId <= 0)
                return BadRequest("Invalid stationId (must be positive).");
            //we should check the station ownership
            var station = repository.GetStationRelatedToAuthToken(authToken, model.StationId.Value);
            if (station == null)
                return BadRequest("Unknown Station Id.");


            if (model.TimeStamp == null)
                return BadRequest("Timestamp is missing.");
            DateTime timestamp = model.TimeStamp.Value;
            if (timestamp == default)
                return BadRequest("Timestamp is invalid.");
            DateTime now = DateTime.Now;
            if (timestamp > now)
                return BadRequest("Timestamp is in the future.");
            if ((now - timestamp).TotalMinutes > 15)
                return BadRequest("Timestamp is too old. Use the delayUpload endpoint.");
            if (model.Sensors == null || model.Sensors.Count == 0)
                return BadRequest("Sensor Data Points Missing.");
            //we should check if sensors are actually related to the stationid
            var validSensorIds = repository
    .GetSensorsRelatedToStation(station.Id)
    .Select(s => s.Id)
    .ToHashSet();

            var invalidSensorIds = model.Sensors
                .Where(s => s.Id == null || !validSensorIds.Contains(s.Id.Value))
                .Select(s => s.Id)
                .ToList();

            if (invalidSensorIds.Count > 0)
                return BadRequest($"The following sensors are invalid or not associated with Station {station.Id}: {string.Join(", ", invalidSensorIds)}");
            

            foreach (var sensor in model.Sensors)
            {
                if (sensor.Id == null || sensor.Id <= 0)
                    return BadRequest("Each sensor must have a valid (positive) Id.");

                if (sensor.Value == null)
                    return BadRequest($"Sensor with Id {sensor.Id} is missing a value.");
            }
            //check if appsettings.json has poolfolder set
            var poolFolder = configuration["PoolFolder"];

            if (string.IsNullOrWhiteSpace(poolFolder))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal error: PoolFolder is not configured.");
            }
            try
            {
                Telemetry.Business.Pool.WriteToPool(model, poolFolder);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal error: Unable to write sensordata.");
            }


            return Ok($"Live Uploaded {model.TimeStamp} for {model.Sensors.Count} Sensors");


        }

        [HttpPost]
        [Route("delayUpload")]
        [RequireAuthKey]
        public async Task<IActionResult> Delay()
        {
            // 1. Check if PrivateKey is present
            if (!HttpContext.Items.TryGetValue("PrivateKey", out var privateKeyObj) || privateKeyObj is not string privateKey)
            {
                return Unauthorized("Private key missing.");
            }
            if (!HttpContext.Items.TryGetValue("AuthToken", out var authTokenObj) || authTokenObj is not string authToken)
            {
                return Unauthorized("Private key missing.");
            }

            // 2. Read body
            string encryptedPayload;
            using (var reader = new StreamReader(Request.Body))
            {
                encryptedPayload = await reader.ReadToEndAsync();
            }

            if (string.IsNullOrWhiteSpace(encryptedPayload))
            {
                return BadRequest("Request body is empty.");
            }

            string jsonPayload;
            try
            {
                jsonPayload = Rsa.DecryptPayload(encryptedPayload, privateKey);
            }
            catch (Exception ex)
            {
                return BadRequest($"Payload decryption failed");
            }

            StationUploadModel? model;
            try
            {
                model = JsonSerializer.Deserialize<StationUploadModel>(jsonPayload, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (JsonException)
            {
                return BadRequest("Invalid JSON format.");
            }
            if (model == null)
            {
                return BadRequest("Payload is null.");
            }

            if (model.StationId == null)
                return BadRequest("Station Id Missing.");
            if (model.StationId <= 0)
                return BadRequest("Invalid stationId (must be positive).");
            //we should check the station ownership
            var station = repository.GetStationRelatedToAuthToken(authToken, model.StationId.Value);
            if (station == null)
                return BadRequest("Unknown Station Id.");


            if (model.TimeStamp == null)
                return BadRequest("Timestamp is missing.");
            DateTime timestamp = model.TimeStamp.Value;
            if (timestamp == default)
                return BadRequest("Timestamp is invalid.");
            DateTime now = DateTime.Now;
            if (timestamp > now)
                return BadRequest("Timestamp is in the future.");
            if ((now - timestamp).TotalMinutes < 15)
                return BadRequest("Timestamp is too recent. Use the liveUpload endpoint.");

            if ((now - timestamp).TotalDays >1)
                return BadRequest("Data older than a day cannot be processed.");
            if (model.Sensors == null || model.Sensors.Count == 0)
                return BadRequest("Sensor Data Points Missing.");
            //we should check if sensors are actually related to the stationid
            var validSensorIds = repository
    .GetSensorsRelatedToStation(station.Id)
    .Select(s => s.Id)
    .ToHashSet();

            var invalidSensorIds = model.Sensors
                .Where(s => s.Id == null || !validSensorIds.Contains(s.Id.Value))
                .Select(s => s.Id)
                .ToList();

            if (invalidSensorIds.Count > 0)
                return BadRequest($"The following sensors are invalid or not associated with Station {station.Id}: {string.Join(", ", invalidSensorIds)}");


            foreach (var sensor in model.Sensors)
            {
                if (sensor.Id == null || sensor.Id <= 0)
                    return BadRequest("Each sensor must have a valid (positive) Id.");

                if (sensor.Value == null)
                    return BadRequest($"Sensor with Id {sensor.Id} is missing a value.");
            }

            //check if appsettings.json has poolfolder set
            var poolFolder = configuration["PoolFolder"];

            if (string.IsNullOrWhiteSpace(poolFolder))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal error: PoolFolder is not configured.");
            }
            try
            {
                Telemetry.Business.Pool.WriteToPool(model, poolFolder);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal error: Unable to write sensordata.");
            }
            return Ok($"Delay Uploaded {model.TimeStamp} for {model.Sensors.Count} Sensors");

        }
    }
}
