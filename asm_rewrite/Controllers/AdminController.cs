using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asm_rewrite.Controllers
{
    public class AdminController : Controller
    {
        [Route("admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("admin/manage-products")]
        public IActionResult Products()
        {
            return View();
        }

        [Route("admin/manage-categories")]
        public IActionResult Categories()
        {
            return View();
        }

        [Route("admin/manage-orders")]
        public IActionResult Orders()
        {
            return View();
        }

        [Route("admin/manage-customers")]
        public IActionResult Customers()
        {
            return View();
        }

        [Route("admin/manage-users")]
        public IActionResult Users()
        {
            return View();
        }
    }
}
