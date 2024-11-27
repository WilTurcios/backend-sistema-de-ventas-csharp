using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    public class SaleDetailsDTO
    {
        public string? Total { get; set; }

        public string? Price { get; set; }

        public int? Quantity { get; set; }

        public int? ProductId { get; set; }
        public string? ProductDescription { get; set; }
    }
}
