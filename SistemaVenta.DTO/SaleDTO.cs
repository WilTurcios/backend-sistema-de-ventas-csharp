using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    public class SaleDTO
    {
        public int Id { get; set; }

        public string? PaymentMethod { get; set; }

        public string? Total { get; set; }

        public string? DocumentNumberId { get; set; }
        
        public string? RegisterDate { get; set; }

        public virtual ICollection<SaleDetailsDTO>? SaleDetails { get; set; }

    }
}
