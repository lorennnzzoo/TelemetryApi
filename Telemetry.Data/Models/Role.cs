using System;
using System.Collections.Generic;

namespace Telemetry.Data.Models;

public partial class Role
{
    public string Role1 { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
