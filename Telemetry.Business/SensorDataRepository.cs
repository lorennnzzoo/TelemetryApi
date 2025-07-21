using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemetry.Data.Dtos;
using Telemetry.Data.Models;

namespace Telemetry.Business
{
    public class SensorDataRepository : ISensorDataRepository
    {
        private readonly TelemetryapiContext context;
        public SensorDataRepository(TelemetryapiContext _context)
        {
            context = _context;
        }
        public void InsertSensorData(StationUploadModel model)
        {
            if (model.TimeStamp == null || model.Sensors == null || model.Sensors.Count == 0)
                throw new ArgumentException("Invalid sensor upload model.");

            var rows = model.Sensors
                .Where(s => s.Id.HasValue && s.Value.HasValue)
                .Select(s => new SensorDatum
                {
                    SensorId = s.Id.Value,
                    Value =Convert.ToDouble( s.Value.Value),
                    Timestamp = model.TimeStamp.Value.ToUniversalTime()
                })
                .ToList();

            if (rows.Count == 0)
                return;

            context.SensorData.AddRange(rows);
            context.SaveChanges();
        }
    }
}
