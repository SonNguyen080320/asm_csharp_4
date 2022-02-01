using asm_rewrite.Models;
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
    public class HomeController : Controller
    {
        private readonly AsmContext context;

        public HomeController(ILogger<HomeController> logger, AsmContext context)
        {
            this.context = context;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {

            var topProducts = await context.Products.Where(p => p.IsTop).ToListAsync();
            var bestSellerProducts = await context.Products.Where(p => p.IsBestSeller).ToListAsync();
            ViewBag.topProducts = topProducts;
            ViewBag.bestSellerProducts = bestSellerProducts;

            return View();
        }

        [Route("/{alias}")]
        public async Task<IActionResult> ProductDetail(string alias)
        {
            var currentProduct = await context.Products.SingleAsync(p => p.Alias == alias);
            ViewBag.currentProduct = currentProduct;

            return View();
        }
    }
}
