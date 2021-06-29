using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleSales.Api.Databases;
using SimpleSales.Api.Exceptions;
using SimpleSales.Api.Models;

namespace SimpleSales.Api.Services
{
    public interface IOrderService
    {
        Task<IList<OrderModel>> GetAsync();
        Task<bool> Create(OrderModel request);
    }

    public class OrderService : IOrderService
    {
        private readonly AppDbContext _appDbContext;

        public OrderService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IList<OrderModel>> GetAsync()
        {
            return await _appDbContext.Order.Include(x => x.Product)
                .Select(x => new OrderModel 
                {
                    Id = x.Id,
                    Date = x.Date,
                    Quantity = x.Quantity,
                    Product = x.Product,
                    UnitPrice = x.UnitPrice
                }).AsNoTracking().ToListAsync();
        }

        public async Task<bool> Create(OrderModel request)
        {
            try
            {
                var product = await _appDbContext.Product.FindAsync(request.ProductId);

                if(product == null) throw new Exception("Product not found");

                if(product.Quantity < request.Quantity)
                    throw new NotEnoughItemQuantityException();

                product.Quantity -= request.Quantity;
                product.UpdatedDate = DateTime.Now;

                bool saveFailed;
                do
                {
                    saveFailed = false;

                    try
                    {
                        request.UnitPrice = product.UnitPrice;
                        await _appDbContext.Order.AddAsync(request);
                        await _appDbContext.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        saveFailed = true;

                        foreach (var entry in ex.Entries)
                        {
                            if (!(entry.Entity is ProductModel)) continue;

                            var databaseValues = await entry.GetDatabaseValuesAsync();

                            entry.OriginalValues.SetValues(databaseValues);

                            product = (ProductModel)databaseValues.ToObject();

                            if (product.Quantity < request.Quantity)
                                throw new NotEnoughItemQuantityException();

                            product.Quantity -= request.Quantity;
                            product.UpdatedDate = DateTime.Now;

                            entry.CurrentValues.SetValues(product);
                        }
                    }
                } while (saveFailed);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
