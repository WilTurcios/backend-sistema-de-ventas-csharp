using System;
using System.Collections.Generic;

namespace SistemaVenta.Model;

public partial class User
{
    public int Id { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? RegisterDate { get; set; }

    public int? RoleId { get; set; }

    public virtual Role? Role { get; set; }
}
