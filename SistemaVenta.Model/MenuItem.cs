using System;
using System.Collections.Generic;

namespace SistemaVenta.Model;

public partial class MenuItem
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Icon { get; set; }

    public string? Url { get; set; }

    public virtual ICollection<RolMenu> RolMenus { get; } = new List<RolMenu>();
}
