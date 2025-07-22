using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemetry.Data.Models;

namespace Telemetry.Data.Dtos
{
    public class IndustryDashboard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<StationDashboard> Stations { get; set; }
    }
    public class StationDashboard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SensorDashboard> Sensors { get; set; }
    }
    public class SensorDashboard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Units { get; set; }
        public SensorDataPoint DataPoint { get; set; }
    }

    public class SensorDataPoint
    {
        public double Value { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
