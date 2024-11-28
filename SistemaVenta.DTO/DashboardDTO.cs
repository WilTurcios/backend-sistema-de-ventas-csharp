using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    public class DashboardDTO
    {
        public int? TotalSales { get; set; }
        public string? TotalIncome { get; set; }
        public int? TotalProducts { get; set; }
        public List<WeekSalesDTO>? SalesLastWeek {  get; set; }
    }
}
