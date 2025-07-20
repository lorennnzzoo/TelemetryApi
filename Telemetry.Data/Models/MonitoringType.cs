using System;
using System.Collections.Generic;

namespace Telemetry.Data.Models;

public partial class MonitoringType
{
    public string MonitoringType1 { get; set; } = null!;

    public virtual ICollection<Station> Stations { get; set; } = new List<Station>();
}
