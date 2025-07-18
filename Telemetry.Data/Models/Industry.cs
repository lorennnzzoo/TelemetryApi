using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Telemetry.Data.Models;

public partial class Industry
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string ContactPerson { get; set; } = null!;

    public string ContactPhone { get; set; } = null!;

    public string ContactEmail { get; set; } = null!;

    public string Address { get; set; } = null!;
    public IndustryCategory Category { get; set; }
    public ICollection<Station> Stations { get; set; } = new List<Station>();

}
