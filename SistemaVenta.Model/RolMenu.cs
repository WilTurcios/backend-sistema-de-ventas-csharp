using System;
using System.Collections.Generic;

namespace SistemaVenta.Model;

public partial class RolMenu
{
    public int Id { get; set; }

    public int? MenuItemId { get; set; }

    public int? RoleId { get; set; }

    public virtual MenuItem? MenuItem { get; set; }

    public virtual Role? Role { get; set; }
}
