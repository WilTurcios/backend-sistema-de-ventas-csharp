using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Services.Contract;
using SistemaVenta.DAL.Repositories.Contract;
using SistemaVenta.DTO;
using SistemaVenta.Model;

namespace SistemaVenta.BLL.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IGenericRepository<SaleDetails> _saleDetailsRepository;
        private readonly IMapper _mapper;

        public SaleService(ISaleRepository saleRepository, IGenericRepository<SaleDetails> saleDetailsRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _saleDetailsRepository = saleDetailsRepository;
            _mapper = mapper;
        }

        public async Task<List<SaleDTO>> History(string searchBy, string saleNumber, string startDate, string endDate)
        {
            IQueryable<Sale> query = await _saleRepository.Query();
            var resultList = new List<Sale>();

            try
            {
                if (searchBy == "date")
                {
                    DateTime parsedStartDate = DateTime.ParseExact(startDate, "dd/MM/yyyy", new CultureInfo("es-MX"));
                    DateTime parsedEndDate = DateTime.ParseExact(endDate, "dd/MM/yyyy", new CultureInfo("es-MX"));

                    resultList = await query
                        .Where(s =>
                            s.RegisterDate.Value.Date >= parsedStartDate && 
                            s.RegisterDate.Value.Date <= parsedEndDate
                        )
                        .Include(saleDetails => saleDetails.SaleDetails)
                        .ThenInclude(p => p.Product)
                        .ToListAsync();

                } else
                {
                    resultList = await query
                        .Where(s => s.DocumentNumber == saleNumber)
                        .Include(saleDetails => saleDetails.SaleDetails)
                        .ThenInclude(p => p.Product)
                        .ToListAsync();
                }

            }
            catch
            {
                throw;
            }
            
            return _mapper.Map<List<SaleDTO>>(resultList);
        }

        public async Task<SaleDTO> Register(SaleDTO entity)
        {
            try
            {
                var mappedSale= _mapper.Map<Sale>(entity);
                var createdSale = await _saleRepository.Register(mappedSale);



                if (createdSale.Id == 0)
                {
                    throw new TaskCanceledException("Error al registrar la venta");
                }

                return _mapper.Map<SaleDTO>(createdSale);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ReportDTO>> Report(string startDate, string endDate)
        {
            IQueryable<SaleDetails> query = await _saleDetailsRepository.Query();
            List<SaleDetails> saleDetailsList = new List<SaleDetails>();

            try
            {
                DateTime parsedStartDate = DateTime.ParseExact(startDate, "dd/MM/yyyy", new CultureInfo("es-MX"));
                DateTime parsedEndDate = DateTime.ParseExact(endDate, "dd/MM/yyyy", new CultureInfo("es-MX"));

               saleDetailsList = await query
                    .Include(p => p.Product)
                    .Include(s => s.Sale)
                    .Where(sd => 
                        sd.Sale.RegisterDate.Value.Date >= parsedStartDate && 
                        sd.Sale.RegisterDate.Value.Date <= parsedEndDate
                    )
                    .ToListAsync();
            }
            catch
            {
                throw;
            }

            return _mapper.Map<List<ReportDTO>>(saleDetailsList);

        }
    }
}
