using System;
using System.Collections.Generic;

namespace Telemetry.Data.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Role { get; set; }

    public string FullName { get; set; } = null!;

    public DateTime? LastLogin { get; set; }

    public int FailedAttempts { get; set; }

    public int? IndustryId { get; set; }

    public virtual Industry? Industry { get; set; }

    public virtual Role? RoleNavigation { get; set; }
}
