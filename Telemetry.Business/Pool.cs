using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Telemetry.Data.Dtos;

namespace Telemetry.Business
{
    public class Pool
    {

        public static void WriteToPool(StationUploadModel data, string poolFolder)
        {
            string fileName = $"station_{data.StationId}_{data.TimeStamp:yyyyMMddHHmmss}.dat";
            string filePath = Path.Combine(poolFolder, fileName);

            if (File.Exists(filePath))
                return; // Skip if file already exists

            string serialized = JsonSerializer.Serialize(data);
            File.WriteAllText(filePath, serialized);
        }

        public static void ProcessPool(string poolFolder,IServiceProvider provider)
        {
            string[] files = Directory.GetFiles(poolFolder,"*.dat");

            foreach(string file in files)
            {
                var processingFile = Path.ChangeExtension(file, ".processing");

                try
                {                  
                    File.Move(file, processingFile);
                    string fileContent = File.ReadAllText(processingFile);
                    StationUploadModel sensormodel = JsonSerializer.Deserialize<StationUploadModel>(fileContent);
                    if (sensormodel == null)
                        throw new Exception("Invalid model deserialization");

                    using var scope = provider.CreateScope();
                    var repository = scope.ServiceProvider.GetRequiredService<ISensorDataRepository>();
                    repository.InsertSensorData(sensormodel);
                    File.Delete(processingFile);
                }
                catch (IOException)
                {
                    continue; 
                }
            }
        }
    }
}
