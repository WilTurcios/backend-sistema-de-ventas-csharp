using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    internal class DashboardDTO
    {
        public string? TotalSales { get; set; }
        public string? TotalIncome { get; set; }
        public List<WeekSalesDTO>? SalesLastWeek {  get; set; }
    }
}
