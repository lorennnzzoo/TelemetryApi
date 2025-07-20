using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemetry.Data.Models;

namespace Telemetry.Data.Dtos
{
    public class SensorDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string MonitoringId { get; set; } = null!;

        public string MeasuringUnit { get; set; } = null!;

        public DateOnly InstalledDate { get; set; }

        public int StationId { get; set; }
        public Telemetry.Data.Models.Sensor CreateModel()
        {
            return new Telemetry.Data.Models.Sensor
            {
                Id = this.Id,
                Name = this.Name,
                Code = this.Code,
                MonitoringId = this.MonitoringId,
                MeasuringUnit = this.MeasuringUnit,
                InstalledDate = this.InstalledDate,
                StationId = this.StationId
            };
        }

        public void UpdateModel(Telemetry.Data.Models.Sensor sensor)
        {
            sensor.Name = this.Name;
            sensor.Code = this.Code;
            sensor.MonitoringId = this.MonitoringId;
            sensor.MeasuringUnit = this.MeasuringUnit;
            sensor.InstalledDate = this.InstalledDate;
            sensor.StationId = this.StationId;
        }

    }
}
