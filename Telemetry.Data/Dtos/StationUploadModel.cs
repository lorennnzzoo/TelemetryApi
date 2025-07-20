using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telemetry.Data.Dtos
{
    public class StationUploadModel
    {
        public int? StationId { get; set; } = null!;
        public DateTime? TimeStamp { get; set; } = null!;
        public List<Sensor>? Sensors { get; set; } = null!;
    }

    public class Sensor
    {
        public int? Id { get; set; } = null!;
        public decimal? Value { get; set; } = null!;
    }
}
