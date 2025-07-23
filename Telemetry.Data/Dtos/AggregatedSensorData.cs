using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telemetry.Data.Dtos
{
    public class AggregatedSensorData
    {
        public DateTime Timestamp { get; set; }  // this will be the rounded bucket
        public int SensorId { get; set; }
        public double Value { get; set; } // or AvgValue if you want to keep the name
    }

}
