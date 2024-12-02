using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.API.Utility;
using SistemaVenta.BLL.Services.Contract;
using SistemaVenta.DTO;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService) { 
            this._roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() { 
            var response = new Response<List<RoleDTO>>();
            try
            {
                response.status = true;
                response.value = await _roleService.GetRoles();

            }
            catch (Exception ex) { 
                response.status = false;
                response.message = ex.Message;
            }

            return Ok(response);
        }
    }
}
