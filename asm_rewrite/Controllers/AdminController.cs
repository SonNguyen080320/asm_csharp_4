using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asm_rewrite.Models;
using Microsoft.EntityFrameworkCore;

namespace asm_rewrite.Controllers
{
    public class AdminController : Controller
    {
        private readonly AsmContext context;

        public AdminController(AsmContext context)
        {
            this.context = context;
        }

        [Route("admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("admin/manage-products")]
        public IActionResult Products(string SearchText = "", int page = 1)
        {   
            List<Product> products = new Product(context).GetAllProducts();
            //

            if (SearchText.Trim() != null && SearchText.Trim() != "")
            {
                products = products.Where(p => p.Name.ToLower().Contains(SearchText.ToLower())).ToList();
            } else
            {
                products = new Product(context).GetAllProducts();
            }

            //
            int pageSize = 5;

            if (page < 1) page = 1;

            int totalItems = products.Count();

            var pager = new Pager(totalItems, page, pageSize);

            int skip = (page - 1) * pageSize;

            var data = products.Skip(skip).Take(pager.PageSize).ToList();

            ViewBag.products = data;
            ViewBag.pager = pager;

            return View();
        }

        [Route("admin/manage-products/add")]
        public IActionResult AddProduct()
        {
            return View();
        }

        [Route("admin/manage-categories")]
        public IActionResult Categories()
        {
            return View();
        }

        //[Route("admin/manage-orders")]
        //public IActionResult Orders()
        //{
        //    return View();
        //}

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
