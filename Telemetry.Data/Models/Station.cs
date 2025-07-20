using System;
using System.Collections.Generic;

namespace Telemetry.Data.Models;

public partial class Station
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string ContactPerson { get; set; } = null!;

    public string ContactPhone { get; set; } = null!;

    public string ContactEmail { get; set; } = null!;

    public int IndustryId { get; set; }

    public string MonitoringType { get; set; } = null!;

    public virtual Industry Industry { get; set; } = null!;

    public virtual MonitoringType MonitoringTypeNavigation { get; set; } = null!;

    public virtual ICollection<Sensor> Sensors { get; set; } = new List<Sensor>();
}
