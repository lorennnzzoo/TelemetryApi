using System.Text;
using System.Text.Json;
using Telemetry.Data.Dtos;

namespace Telemetry.SensorDataUploader
{
    public class SensorDataUploader : BackgroundService
    {
        private readonly ILogger<SensorDataUploader> _logger;
        private readonly IConfiguration configuration;

        public SensorDataUploader(ILogger<SensorDataUploader> logger, IConfiguration _configuration)
        {
            _logger = logger;
            configuration = _configuration;
        }
        //string apiUrl = "https://127.0.0.1";
        //string publicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAwoVq00oO2HGOAwpnRxktJnc/dIMOAFqtjI31TCCRMxEQkbmKtp66kqAwHFlxXH1GaTpJYMKYot2qhZqsaZ3legFYgJIS41cMu7VjcpbnMxTKqR0SqTZU6gD2p5V8l9Nxl2cO/HWs+J5cOPI9PV6tKBQy+C14ZTROQlnCBmdWlaykOu03LX4JLz5AfXZz2cs99stf+2E3HwYuuaHDKGWkw5iCyocTcXKYVwTpGHdvgdcDWk2MzXiTDaL5wkSYTAwJtcEmpUC99CvL/NDymje78WJdXaqFZODgkdC5h6yq9dQQ//ClwTAcTc5KbrHNFEdniefgyy2CPOFsCav0dXJc6QIDAQAB";
        //string authToken = "7e808e1e3c3c60724cd5ada59893fe5fa12405d5f12a544542cb07dd155e81df";

        HttpClient client = new HttpClient();
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

                string endpoint = $"{configuration["apiUrl"]}/api/upload/liveupload";

                StationUploadModel payload = new StationUploadModel
                {
                    StationId = 2,
                    TimeStamp = DateTime.Now,
                    Sensors = new List<Sensor>
                {
                    new Sensor
                    {
                        Id=2,
                        Value=10
                    }
                }
                };
                string rawPayload = JsonSerializer.Serialize(payload);

                string encryptedPayload = Telemetry.Business.Rsa.EncryptPayload(rawPayload, configuration["apiPublicKey"]);


                var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
                request.Headers.Add("AuthToken", configuration["apiAuthToken"]);
                request.Content = new StringContent(encryptedPayload, Encoding.UTF8, "text/plain");

                var response = client.SendAsync(request).Result;
                //_logger.LogInformation(response.Content.ToString());
                

                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}
