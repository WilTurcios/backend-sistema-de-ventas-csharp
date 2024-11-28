using System;
using System.Collections.Generic;
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
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDTO> Create(ProductDTO entity)
        {
            try
            {
                var createdProduct = await _productRepository.Create(_mapper.Map<Product>(entity));

                if(createdProduct.Id == 0)
                {
                    throw new TaskCanceledException("No se pudo crear el producto");
                }

                return _mapper.Map<ProductDTO>(createdProduct);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var foundProduct = await _productRepository.Get(p => p.Id == id);

                if (foundProduct == null)
                {
                    throw new TaskCanceledException("Producto no encontrado");
                }


                bool result = await _productRepository.Delete(foundProduct);

                if (!result)
                {
                    throw new TaskCanceledException("No se pudo eliminar el producto");
                }

                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ProductDTO>> GetAll()
        {
            try
            {
                var productsQuery = await _productRepository.Query();
                var productsList = productsQuery.Include(category => category.Category).ToList();

                return _mapper.Map<List<ProductDTO>>(productsList);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Update(ProductDTO entity)
        {
            try
            {
                var mappedProduct = _mapper.Map<Product>(entity);
                var foundProduct= await _productRepository.Get(p => p.Id == mappedProduct.Id);



                if (foundProduct == null)
                {
                    throw new TaskCanceledException("Producto no encontrado");
                }

                foundProduct.Name = mappedProduct.Name;
                foundProduct.CategoryId = mappedProduct.CategoryId;
                foundProduct.Stock = mappedProduct.Stock;
                foundProduct.Price = mappedProduct.Price;
                foundProduct.IsActive = mappedProduct.IsActive;

                bool result = await _productRepository.Update(foundProduct);

                if (!result) {
                    throw new TaskCanceledException("No se pudo editar el producto");
                }

                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}
