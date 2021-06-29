using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleSales.WebAdmin.Models;
using SimpleSales.WebAdmin.Models.User;

namespace SimpleSales.WebAdmin.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
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
                var users = await _userManager.Users.ToListAsync();

                response.data = users;
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
        public async Task<JsonResult> Create([FromBody] CreateUserViewModel request)
        {
            var response = new ResponseViewModel { status = true, message = "User added successfully" };

            try
            {
                var user = new IdentityUser
                {
                    UserName = request.Email,
                    Email = request.Email,
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        throw new Exception(error.Description);
                    }
                };

                response.data = request;
                return Json(response);
            }
            catch (Exception e)
            {
                response.status = false;
                response.message = e.Message;
                return Json(response);
            }
        }

        [HttpGet("User/Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] string id)
        {
            IdentityUser user = new IdentityUser();
            var result = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == id);
            user = result ?? user;
            ViewBag.User = user;
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Update([FromBody] UpdateUserViewModel request)
        {
            var response = new ResponseViewModel { status = true, message = "User updated successfully" };

            try
            {
                var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == request.Id);

                if (user == null) throw new Exception("Data not found");

                user.Email = request.Email;
                user.UserName = request.Email;
                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        throw new Exception(error.Description);
                    }
                };

                response.data = user;
                return Json(response);
            }
            catch (Exception e)
            {
                response.status = false;
                response.message = e.Message;
                return Json(response);
            }
        }

        [HttpDelete("User/Delete/{id}")]
        public async Task<JsonResult> Delete([FromRoute] string id)
        {
            var response = new ResponseViewModel { status = true, message = "User deleted successfully" };

            try
            {
                var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == id);

                if (user == null) throw new Exception("Data not found");

                var result = await _userManager.DeleteAsync(user);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        throw new Exception(error.Description);
                    }
                };

                response.data = user;
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
