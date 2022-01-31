using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asm_rewrite.Controllers
{
    public class AccountController : Controller
    {
        [Route("sign-in")]
        public IActionResult SignIn()
        {
            return View();
        }

        [Route("sign-up")]
        public IActionResult SignUp()
        {
            return View();
        }
    }
}
