using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asm_rewrite.Controllers
{
    public class CartController : Controller
    {
        [Route("cart")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
