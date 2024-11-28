using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SistemaVenta.BLL.Services.Contract;
using SistemaVenta.DAL.Repositories.Contract;
using SistemaVenta.DTO;
using SistemaVenta.Model;


namespace SistemaVenta.BLL.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ISaleRepository _saleReporitory;
        private readonly IGenericRepository<Product> _productReporitory;
        private readonly IMapper _mapper;

        public DashboardService(IGenericRepository<Product> productReporitory, ISaleRepository saleReporitory, IMapper mapper)
        {
            _productReporitory = productReporitory;
            _saleReporitory = saleReporitory;
            _mapper = mapper;
        }

        private IQueryable<Sale> GetSales(IQueryable<Sale> saleTable, int days)
        {
            DateTime? lastDate = saleTable.OrderByDescending(s => s.RegisterDate).Select(s => s.RegisterDate).First();

            lastDate = lastDate.Value.AddDays(days);

            return saleTable.Where(s => s.RegisterDate.Value.Date >= lastDate.Value.Date);
        }

        private async Task<int> TotalSalesLastWeek()
        {
            int total = 0;
            IQueryable<Sale> _saleQuery = await _saleReporitory.Query();

            if (_saleQuery.Count() > 0) {
                var saleTable = GetSales(_saleQuery, -7);
                total = saleTable.Count();
            }

            return total;
        }

        private async Task<string> TotalIncomeLastWeek()
        {
            decimal total = 0;
            IQueryable<Sale> _saleQuery = await _saleReporitory.Query();

            if (_saleQuery.Count() > 0)
            {
                var saleTable = GetSales(_saleQuery, -7);

                total = saleTable.Select(s => s.Total).Sum(s => s.Value);

            }

            return Convert.ToString(total, new CultureInfo("es-MX")); 
        }

        private async Task<int> TotalProducts()
        {
            IQueryable<Product> _productQuery = await _productReporitory.Query();

            int total = _productQuery.Count();
            return total;
        }

        private async Task<Dictionary<string, int>> SalesLaskWeek()
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            IQueryable<Sale> _salesQuery = await _saleReporitory.Query();

            if( _salesQuery.Count() > 0)
            {
                var salesTable = GetSales(_salesQuery, -7);

                result = salesTable
                    .GroupBy(s => s.RegisterDate.Value.Date)
                    .OrderBy(r => r.Key)
                    .Select(sd => new {date = sd.Key.ToString("dd/MM/yyyy"), total = sd.Count()})
                    .ToDictionary(keySelector: r => r.date, elementSelector: r => r.total);
            }

            return result;
        }
        public async Task<DashboardDTO> Summary()
        {
            DashboardDTO dashboardDTO = new DashboardDTO();

            try
            {
                dashboardDTO.TotalSales = await TotalSalesLastWeek();
                dashboardDTO.TotalIncome = await TotalIncomeLastWeek();
                dashboardDTO.TotalProducts = await TotalProducts();

                List<WeekSalesDTO> weekSalesList = new List<WeekSalesDTO>();

                foreach(KeyValuePair<string, int> item in await SalesLaskWeek())
                {
                    weekSalesList.Add(new WeekSalesDTO()
                    {
                        Date = item.Key,
                        Total = item.Value
                    });

                    dashboardDTO.SalesLastWeek = weekSalesList;
                }

                return dashboardDTO;
            }
            catch
            {
                throw;
            }
        }
    }
}
