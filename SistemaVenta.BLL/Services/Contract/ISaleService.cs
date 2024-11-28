using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.DTO;

namespace SistemaVenta.BLL.Services.Contract
{
    public interface ISaleService
    {
        Task<List<SaleDTO>> History(string searchBy, string saleNumber, string startDate, string endDate);
        Task<SaleDTO> Register(SaleDTO entity);
        Task<List<ReportDTO>> Report(string startDate, string endDate);

    }
}
