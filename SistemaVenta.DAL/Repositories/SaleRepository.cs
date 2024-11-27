using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.DAL.DBContext;
using SistemaVenta.DAL.Repositories.Contract;
using SistemaVenta.Model;

namespace SistemaVenta.DAL.Repositories
{
    public class SaleRepository : GenericRepository<Sale>, ISaleRepository
    {
        private readonly DbsalesContext _dbcontext;

        public SaleRepository(DbsalesContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Sale> Register(Sale entity)
        {
            Sale new_sale = new Sale();

            using (var transaction = _dbcontext.Database.BeginTransaction()) {

                try
                {
                    foreach (SaleDetails sale_details in entity.SaleDetails)
                    {
                        Product found_product =  _dbcontext.Products.Where(p => p.Id == sale_details.ProductId).First();
                        found_product.Stock = found_product.Stock - sale_details.Quantity;

                        _dbcontext.Products.Update(found_product);
                    }

                    await _dbcontext.SaveChangesAsync();

                    DocumentNumber correlative = _dbcontext.DocumentNumbers.First();

                    correlative.LastNumber = correlative.LastNumber + 1;
                    correlative.RegisterDate = DateTime.Now;

                    _dbcontext.DocumentNumbers.Update(correlative);
                    await _dbcontext.SaveChangesAsync();

                    int digits = 4;
                    string zeros = string.Concat(Enumerable.Repeat("0", digits));
                    string sale_number = zeros + correlative.LastNumber.ToString();                    

                    sale_number = sale_number.Substring(sale_number.Length - digits, digits);

                    entity.DocumentNumber = sale_number;

                    await _dbcontext.Sales.AddAsync(entity);
                    await _dbcontext.SaveChangesAsync();

                    new_sale = entity;

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return new_sale;
        }
    }
}
