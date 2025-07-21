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
    }
}
