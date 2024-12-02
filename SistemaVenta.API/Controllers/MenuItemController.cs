using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.BLL.Services;
using SistemaVenta.BLL.Services.Contract;
using SistemaVenta.DTO;
using SistemaVenta.API.Utility;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;

        public MenuItemController(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMenuItemsByUserId(int id)
        {
            var response = new Response<List<MenuItemDTO>>();
            try
            {
                response.status = true;
                response.value = await _menuItemService.GetMenuItems(id);

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
