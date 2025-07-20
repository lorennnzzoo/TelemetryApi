using System;
using System.Collections.Generic;

namespace Telemetry.Data.Models;

public partial class Sensor
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string MonitoringId { get; set; } = null!;

    public string MeasuringUnit { get; set; } = null!;

    public DateOnly InstalledDate { get; set; }

    public int StationId { get; set; }

    public virtual Station Station { get; set; } = null!;
}
