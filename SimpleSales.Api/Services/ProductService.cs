
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleSales.Api.Databases;
using SimpleSales.Api.Models;

namespace SimpleSales.Api.Services
{
    public interface IProductService
    {
        Task<IList<ProductModel>> GetAsync();
    }

    public class ProductService : IProductService
    {
        private readonly AppDbContext _appDbContext;

        public ProductService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IList<ProductModel>> GetAsync()
        {
            return await _appDbContext.Product.Select(x => new ProductModel
            {
                Id = x.Id,
                Name = x.Name,
                UnitPrice = x.UnitPrice,
                Quantity = x.Quantity
            }).AsNoTracking().ToListAsync();
        }
    }
}
