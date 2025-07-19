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

    public string Category { get; set; } = null!;
    [JsonIgnore]
    public virtual Category CategoryNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Key> Keys { get; set; } = new List<Key>();
    [JsonIgnore]
    public virtual ICollection<Station> Stations { get; set; } = new List<Station>();
}
