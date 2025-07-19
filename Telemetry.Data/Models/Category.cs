using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Telemetry.Data.Models;

public partial class Category
{
    public string Category1 { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Industry> Industries { get; set; } = new List<Industry>();
}
