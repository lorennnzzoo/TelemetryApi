using System;
using System.Collections.Generic;

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

    public virtual Category CategoryNavigation { get; set; } = null!;

    public virtual ICollection<Key> Keys { get; set; } = new List<Key>();

    public virtual ICollection<Station> Stations { get; set; } = new List<Station>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
