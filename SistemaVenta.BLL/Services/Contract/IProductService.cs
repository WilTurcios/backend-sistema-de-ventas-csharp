using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.DTO;

namespace SistemaVenta.BLL.Services.Contract
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAll();
        Task<ProductDTO> Create(ProductDTO entity);
        Task<bool> Update(ProductDTO entity);
        Task<bool> Delete(int id);
    }
}
