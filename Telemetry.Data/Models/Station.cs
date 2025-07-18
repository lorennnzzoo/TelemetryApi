using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Telemetry.Data.Models;

public partial class Station
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string ContactPerson { get; set; } = null!;

    public string ContactPhone { get; set; } = null!;

    public string ContactEmail { get; set; } = null!;
    public MonitoringType MonitoringType { get; set; }
    public int IndustryId { get; set; }
    
    public virtual Industry Industry { get; set; } = null!;

    public  ICollection<Sensor> Sensors { get; set; } = new List<Sensor>();

}
