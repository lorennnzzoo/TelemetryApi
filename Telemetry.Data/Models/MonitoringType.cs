using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Telemetry.Data.Models;

public partial class MonitoringType
{
    public string MonitoringType1 { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Station> Stations { get; set; } = new List<Station>();
}
