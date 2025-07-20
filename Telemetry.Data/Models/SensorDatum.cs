using System;
using System.Collections.Generic;

namespace Telemetry.Data.Models;

public partial class SensorDatum
{
    public long Id { get; set; }

    public int? SensorId { get; set; }

    public DateTime Timestamp { get; set; }

    public double Value { get; set; }
}
