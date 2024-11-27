using System;
using System.Collections.Generic;

namespace SistemaVenta.Model;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Stock { get; set; }

    public decimal? Price { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? RegisterDate { get; set; }

    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<SaleDetails> SalesDetails { get; } = new List<SaleDetails>();
}
