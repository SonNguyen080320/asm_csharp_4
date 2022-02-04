using asm_rewrite.Models;
using asm_rewrite.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace asm_rewrite.Controllers
{
    public class ProductController : Controller
    {
        private readonly AsmContext context;
        public ProductController(AsmContext context)
        {
            this.context = context;
        }

        [Route("/")]
        public IActionResult Index()
        {
            var topProducts = new Product(context).GetTopProducts();
            var bestSellerProducts = new Product(context).GetBestSellerProducts();
            var categories = new Category(context).GetAllCategories();
            var cart = SessionExtensions.GetSessionData<List<Item>>(HttpContext.Session, "cart");

            ViewBag.topProducts = topProducts;
            ViewBag.bestSellerProducts = bestSellerProducts;
            ViewBag.categories = categories;
            ViewBag.cart = cart;

            return View();
        }

        [Route("/{alias}")]
        public IActionResult ProductDetail(string alias)
        {
            var currentProduct = new Product(context).GetCurrentProduct(alias);
            var categories = new Category(context).GetAllCategories();
            var cart = SessionExtensions.GetSessionData<List<Item>>(HttpContext.Session, "cart");

            ViewBag.currentProduct = currentProduct;
            ViewBag.categories = categories;
            ViewBag.cart = cart;

            return View();
        }
    }
}
