using System;
using System.Collections.Generic;

namespace Telemetry.Data.Models;

public partial class Key
{
    public int IndustryId { get; set; }

    public string AuthKey { get; set; } = null!;

    public string PublicKey { get; set; } = null!;

    public string PrivateKey { get; set; } = null!;

    public virtual Industry Industry { get; set; } = null!;
}
