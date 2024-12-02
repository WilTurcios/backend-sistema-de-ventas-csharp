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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) { 
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var response = new Response<List<UserDTO>>();
            try
            {
                response.status = true;
                response.value = await _userService.Query();

            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = ex.Message;
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            var response = new Response<SessionDTO>();
            try
            {
                response.status = true;
                response.value = await _userService.ValidateCredentials(
                    loginRequest.Email, 
                    loginRequest.Password
                );
            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = ex.Message;
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO newUser)
        {
            var response = new Response<UserDTO>();
            try
            {
                response.status = true;
                response.value = await _userService.Create(newUser);
            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = ex.Message;
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserDTO userToUpdate)
        {
            var response = new Response<bool>();
            try
            {
                response.status = true;
                response.value = await _userService.Update(userToUpdate);
            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = ex.Message;
            }

            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = new Response<bool>();
            try
            {
                response.status = true;
                response.value = await _userService.Delete(id);
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
