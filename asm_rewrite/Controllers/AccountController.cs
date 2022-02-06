﻿using asm_rewrite.Enums;
using asm_rewrite.Models;
using asm_rewrite.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        public async Task<IActionResult> SignIn(SignIn user)
        {
            var existsEmail = await context.Users.SingleOrDefaultAsync(u => u.Email == user.Email);
            
            if (ModelState.IsValid && existsEmail != null 
                && user.Password == existsEmail.Password)
            {
                // 
                var userRole = await context.Roles.SingleOrDefaultAsync(r => r.Id == existsEmail.RoleId);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, existsEmail.Email),
                    new Claim(ClaimTypes.MobilePhone, existsEmail.Phone),
                    new Claim(ClaimTypes.Name, existsEmail.FirstName),
                };

                var claimsIdentity = new ClaimsIdentity(claims, "cookie");

                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new[] { claimsIdentity });

                await HttpContext.SignInAsync(
                    scheme: "Grandmas.Cookie",
                    principal: claimsPrincipal,
                    properties: new AuthenticationProperties
                    {
                        //IsPersistent = true, // for 'remember me' feature
                        //ExpiresUtc = DateTime.UtcNow.AddMinutes(1)
                    });

                if (userRole.Name.ToLower() == "admin")
                {
                    //
                    return RedirectToAction("index", "admin");
                }
                else if (userRole.Name.ToLower() == "employee")
                {
                    //
                    return RedirectToAction("index", "admin");
                }

                return RedirectToAction("home", "product");
            }

            ModelState.TryAddModelError("CustomSignInError", "Email hoặc mật khẩu không chính xác");
            return View(user);
        }

        [Route("sign-up")]
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [Route("sign-up")]
        [HttpPost]
        public async Task<IActionResult> SignUp(User user)
        {
            var existsPhone = await context.Users.SingleOrDefaultAsync(u => u.Phone == user.Phone);
            var existsEmail = await context.Users.SingleOrDefaultAsync(u => u.Email == user.Email);

            if (existsPhone != null)
            {
                ModelState.TryAddModelError("CustomSignUpError", "Số điện thoại đã tồn tại");
            }
            else if (existsEmail != null)
            {
                ModelState.TryAddModelError("CustomSignUpError", "Email đã tồn tại");
            }
            else if (ModelState.IsValid)
            {
                var customerRole = await context.Roles.Where(r => r.Name == "customer").ToListAsync();

                var newUser = new User
                {
                    LastName = user.LastName ?? null,
                    FirstName = user.FirstName,
                    Phone = user.Phone,
                    Email = user.Email,
                    Password = user.Password,
                    RoleId = customerRole[0].Id
                };

                await context.Users.AddAsync(newUser);
                await context.SaveChangesAsync();

                TempData["sign-up__alert--success"] = AlertExtensions.ShowAlert(Alerts.Success, "Đăng ký thành công");

                return RedirectToAction("signIn", "account");
            }

            return View(user);
        }

        [Route("sign-out")]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(scheme: "Grandmas.Cookie");
            return RedirectToAction("signIn", "account");
        }
    }
}
