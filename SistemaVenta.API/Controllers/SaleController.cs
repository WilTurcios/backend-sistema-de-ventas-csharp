using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.BLL.Services.Contract;
using SistemaVenta.DTO;
using SistemaVenta.Model;
using SistemaVenta.API.Utility;
using AutoMapper;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;
        private readonly IMapper _mapper;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        [Route("history")]
        public async Task<IActionResult> History(
            string searchBy, 
            string? saleNumber, 
            string? startDate, 
            string? endDate
          )
        {
            var response = new Response<List<SaleDTO>>();
            saleNumber = saleNumber ?? string.Empty;
            startDate = startDate ?? string.Empty;
            endDate = endDate ?? string.Empty;

            try
            {
                response.status = true;
                response.value = await _saleService.History(searchBy, saleNumber, startDate, endDate);

            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = ex.Message;
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterSale([FromBody] SaleDTO newSale)
        {
            var response = new Response<SaleDTO>();
            try
            {
                response.status = true;
                response.value = await _saleService.Register(newSale);
            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = ex.Message;
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("report")]
        public async Task<IActionResult> Report(string? startDate, string? endDate)
        {
            var response = new Response<List<ReportDTO>>();

            try
            {
                response.status = true;
                response.value = await _saleService.Report(startDate, endDate);

            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = ex.Message;
            }

            return Ok(response);
        }
    }
}
