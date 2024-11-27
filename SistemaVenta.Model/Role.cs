using System;
using System.Collections.Generic;

namespace SistemaVenta.Model;

public partial class Role
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime? RegisterDate { get; set; }

    public virtual ICollection<RolMenu> RolMenus { get; } = new List<RolMenu>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
