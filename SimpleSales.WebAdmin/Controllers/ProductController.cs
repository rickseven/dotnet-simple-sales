using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleSales.WebAdmin.Databases;
using SimpleSales.WebAdmin.Models;
using SimpleSales.WebAdmin.Models.Product;

namespace SimpleSales.WebAdmin.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AppDBContext _appDbContext;

        public ProductController(SignInManager<IdentityUser> signInManager, AppDBContext appDbContext)
        {
            _signInManager = signInManager;
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> Get()
        {
            var response = new ResponseViewModel { status = true, message = "Success" };

            try
            {
                var products = await _appDbContext.Product.ToListAsync();

                response.data = products;
                return Json(response);
            }
            catch (Exception e)
            {
                response.status = false;
                response.message = e.Message;
                return Json(response);
            }
            
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Create([FromBody] CreateProductViewModel request)
        {
            var response = new ResponseViewModel { status = true, message = "Product added successfully" };

            try
            {
                var loggedUserName = _signInManager.Context.User.Identity.Name;
                var product = new ProductModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = request.Name,
                    UnitPrice = request.UnitPrice,
                    Quantity = request.Quantity,
                    CreatedBy = loggedUserName,
                    CreatedDate = DateTime.Now
                };

                await _appDbContext.Product.AddAsync(product);
                await _appDbContext.SaveChangesAsync();

                response.data = product;
                return Json(response);
            }
            catch (Exception e)
            {
                response.status = false;
                response.message = e.Message;
                return Json(response);
            }
        }

        [HttpGet("Product/Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] string id)
        {
            var product = new ProductModel();
            var result = await _appDbContext.Product.SingleOrDefaultAsync(u => u.Id == id);
            product = result ?? product;
            ViewBag.Product = product;
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Update([FromBody] UpdateProductViewModel request)
        {
            var response = new ResponseViewModel { status = true, message = "Product updated successfully" };

            try
            {
                var product = await _appDbContext.Product.FindAsync(request.Id);

                if (product == null) throw new Exception("Data not found");

                product.Name = request.Name;
                product.UnitPrice = request.UnitPrice;
                product.Quantity = request.Quantity;
                await _appDbContext.SaveChangesAsync();

                response.data = product;
                return Json(response);
            }
            catch (Exception e)
            {
                response.status = false;
                response.message = e.Message;
                return Json(response);
            }
        }

        [HttpDelete("Product/Delete/{id}")]
        public async Task<JsonResult> Delete([FromRoute] string id)
        {
            var response = new ResponseViewModel { status = true, message = "Product deleted successfully" };

            try
            {
                var product = await _appDbContext.Product.FindAsync(id);

                if (product == null) throw new Exception("Data not found");

                _appDbContext.Remove(product);
                await _appDbContext.SaveChangesAsync();

                response.data = product;
                return Json(response);
            }
            catch (Exception e)
            {
                response.status = false;
                response.message = e.Message;
                return Json(response);
            }
        }
    }
}
