using asm_rewrite.Enums;
using asm_rewrite.Models;
using asm_rewrite.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asm_rewrite.Controllers
{
    public class AccountController : Controller
    {
        private readonly AsmContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public AccountController(AsmContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        [Route("sign-in")]
        public IActionResult SignIn()
        {
            return View();
        }

        [Route("sign-in")]
        [HttpPost]
        public IActionResult SignIn(User user)
        {
            return View();
        }

        [Route("sign-up")]
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [Route("sign-up")]
        [HttpPost]
        public IActionResult SignUp(User user)
        {
            //var existsPhone = await context.Users.Where(u => u.Phone == user.Phone).ToListAsync();
            //var existsEmail = await context.Users.Where(u => u.Email == user.Email).ToListAsync();

            //if (existsPhone != null)
            //{
            //    TempData["alert"] = new Alert("Số điện thoại đã tồn tại", "danger");
            //}
            //else if (existsEmail != null)
            //{
            //    TempData["alert"] = new Alert("Email đã tồn tại", "danger");
            //} 
            //else
            
            if(ModelState.IsValid)
            {
                var newUser = new User
                {
                    LastName = user.LastName,
                    FirstName = user.FirstName,
                    Phone = user.Phone,
                    Email = user.Email,
                    Password = user.Password
                };

                TempData["sign-up__alert"] = AlertExtensions.ShowAlert(Alerts.Success, "Đăng ký thành công");

                return RedirectToAction("signIn", "account");
            }


            return View();
        }
    }
}
