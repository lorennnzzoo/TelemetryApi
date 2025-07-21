using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Text;
using Telemetry.Data.Dtos;
using static System.Net.WebRequestMethods;

namespace Telemetry.Api.Test
{
    public class UploadTests 
    {
        string apiUrl = "https://localhost:7138";
        string publicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAwoVq00oO2HGOAwpnRxktJnc/dIMOAFqtjI31TCCRMxEQkbmKtp66kqAwHFlxXH1GaTpJYMKYot2qhZqsaZ3legFYgJIS41cMu7VjcpbnMxTKqR0SqTZU6gD2p5V8l9Nxl2cO/HWs+J5cOPI9PV6tKBQy+C14ZTROQlnCBmdWlaykOu03LX4JLz5AfXZz2cs99stf+2E3HwYuuaHDKGWkw5iCyocTcXKYVwTpGHdvgdcDWk2MzXiTDaL5wkSYTAwJtcEmpUC99CvL/NDymje78WJdXaqFZODgkdC5h6yq9dQQ//ClwTAcTc5KbrHNFEdniefgyy2CPOFsCav0dXJc6QIDAQAB";
        string authToken = "7e808e1e3c3c60724cd5ada59893fe5fa12405d5f12a544542cb07dd155e81df";

        HttpClient client = new HttpClient();
        [Fact]
        public void LiveUpload()
        {
            string endpoint = $"{apiUrl}/api/upload/liveupload";

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
            string rawPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);

            string encryptedPayload = Telemetry.Business.Rsa.EncryptPayload(rawPayload, publicKey);


            var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
            request.Headers.Add("AuthToken", authToken);
            request.Content = new StringContent(encryptedPayload, Encoding.UTF8, "text/plain");

            var response = client.SendAsync(request).Result;
            Console.WriteLine(response.Content);
            response.EnsureSuccessStatusCode();
        }
    }
}