using System;
using System.Collections.Generic;

namespace Telemetry.Data.Models;

public partial class Category
{
    public string Category1 { get; set; } = null!;

    public virtual ICollection<Industry> Industries { get; set; } = new List<Industry>();
}
