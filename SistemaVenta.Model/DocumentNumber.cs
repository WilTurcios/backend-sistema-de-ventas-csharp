using System;
using System.Collections.Generic;

namespace SistemaVenta.Model;

public partial class DocumentNumber
{
    public int Id { get; set; }

    public int LastNumber { get; set; }

    public DateTime? RegisterDate { get; set; }

    public virtual ICollection<Sale> Sales { get; } = new List<Sale>();

    public static implicit operator DocumentNumber(string v)
    {
        throw new NotImplementedException();
    }
}
