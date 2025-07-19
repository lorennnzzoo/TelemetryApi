using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Telemetry.Data.Models;

public partial class Key
{
    public int IndustryId { get; set; }

    public string AuthKey { get; set; } = null!;
    [JsonIgnore]
    public virtual Industry Industry { get; set; } = null!;
}
