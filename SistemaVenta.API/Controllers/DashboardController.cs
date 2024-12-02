using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.DTO;
using SistemaVenta.API.Utility;
using SistemaVenta.BLL.Services.Contract;


namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {

        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        [Route("summary")]
        public async Task<IActionResult> Summary()
        {
            var response = new Response<DashboardDTO>();
            try
            {
                response.status = true;
                response.value = await _dashboardService.Summary();

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
