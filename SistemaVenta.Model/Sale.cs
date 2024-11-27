using System;
using System.Collections.Generic;

namespace SistemaVenta.Model;

public partial class Sale
{
    public int Id { get; set; }

    public string? PaymentMethod { get; set; }

    public decimal? Total { get; set; }

    public int? DocumentNumberId { get; set; }

    public virtual DocumentNumber? DocumentNumber { get; set; }

    public virtual ICollection<SaleDetails> SaleDetails { get; } = new List<SaleDetails>();
}
