using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SimpleSales.WebAdmin.Databases;
using SimpleSales.WebAdmin.Models;
using SimpleSales.WebAdmin.Models.Order;

namespace SimpleSales.WebAdmin.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly AppDBContext _appDbContext;

        public ReportController(AppDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetChartData([FromQuery] string start, [FromQuery] string end)
        {
            var response = new ResponseViewModel { status = true, message = "Success" };

            try
            {
                DateTime.TryParse(start, out var startDate);
                DateTime.TryParse(end, out var endDate);
                var absoluteEndDate = endDate.AddDays(1).AddTicks(-1);
                
                var orders = await _appDbContext.Order.Where(x => x.Date >= startDate && x.Date <= absoluteEndDate).GroupBy(x => x.Date.Month).Select(x => new { Month = x.Key, Count = x.Sum(s => (s.Quantity * s.UnitPrice)) }).ToListAsync();
                
                response.data = orders;
                return Json(response);
            }
            catch (Exception e)
            {
                response.status = false;
                response.message = e.Message;
                return Json(response);
            }
        }

        public async Task<JsonResult> Get([FromQuery] string start, [FromQuery] string end)
        {
            var response = new ResponseViewModel { status = true, message = "Success" };

            try
            {
                DateTime.TryParse(start, out var startDate);
                DateTime.TryParse(end, out var endDate);
                var absoluteEndDate = endDate.AddDays(1).AddTicks(-1);

                var orders = await _appDbContext.Order
                    .Where(x => x.Date >= startDate && x.Date <= absoluteEndDate)
                    .Include(x => x.Product)
                    .Select(x => new OrderModel
                    {
                        Id = x.Id,
                        Date = x.Date,
                        Quantity = x.Quantity,
                        Product = x.Product,
                        UnitPrice = x.UnitPrice
                    })
                    .ToListAsync();

                response.data = orders;
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
