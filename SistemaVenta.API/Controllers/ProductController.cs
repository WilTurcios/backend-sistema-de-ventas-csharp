using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.BLL.Services.Contract;
using SistemaVenta.DTO;
using SistemaVenta.API.Utility;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = new Response<List<ProductDTO>>();
            try
            {
                response.status = true;
                response.value = await _productService.GetAll();

            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = ex.Message;
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDTO newProduct)
        {
            var response = new Response<ProductDTO>();
            try
            {
                response.status = true;
                response.value = await _productService.Create(newProduct);
            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = ex.Message;
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDTO productToUpdate)
        {
            var response = new Response<bool>();
            try
            {
                response.status = true;
                response.value = await _productService.Update(productToUpdate);
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
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = new Response<bool>();
            try
            {
                response.status = true;
                response.value = await _productService.Delete(id);
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
