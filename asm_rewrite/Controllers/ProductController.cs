using asm_rewrite.Models;
using asm_rewrite.Utils;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductController(AsmContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        // Home page - GET
        [Route("/")]
        public IActionResult Index()
        {
            var topProducts = context.Products.Where(p => p.IsTop).ToList();
            var bestSellerProducts = context.Products.Where(p => p.IsBestSeller).ToList();
            var categories = new Category(context).GetAllCategories();
            var cart = SessionExtensions.GetSessionData<List<Item>>(HttpContext.Session, "cart");

            ViewBag.topProducts = topProducts;
            ViewBag.bestSellerProducts = bestSellerProducts;
            ViewBag.categories = categories;
            ViewBag.cart = cart;

            return View();
        }

        // Product detail - GET
        [Route("/{alias}")]
        public IActionResult ProductDetail(string alias)
        {
            var currentProduct = context.Products.Single(p => p.Alias == alias);
            var categories = new Category(context).GetAllCategories();
            var cart = SessionExtensions.GetSessionData<List<Item>>(HttpContext.Session, "cart");

            ViewBag.currentProduct = currentProduct;
            ViewBag.categories = categories;
            ViewBag.cart = cart;

            return View();
        }

        // Product - ADD
        [Route("admin/manage-products/index")]
        [HttpPost]
        public async Task<IActionResult> AddProduct(
            [Bind("CategoryId, Name, Alias, Brand, ImportPrice, Price, HotPrice, Quantity, Description, Image, IsDeleted, IsTop, IsBestSeller, IsFreeShip, ImageFile")] Product product)
        {
            if (ModelState.IsValid)
            {
                string imgPath = FileExtension.UploadFile(product.ImageFile, webHostEnvironment);

                var newProduct = new Product
                {
                    Name = product.Name,
                    CategoryId = product.CategoryId,
                    Alias = product.Alias,
                    Brand = product.Brand,
                    ImportPrice = product.ImportPrice,
                    Price = product.Price,
                    HotPrice = product.HotPrice != null ? product.HotPrice : null,
                    Quantity = product.Quantity,
                    Description = product.Description,
                    Image = imgPath,
                    IsDeleted = product.IsDeleted,
                    IsTop = product.IsTop,
                    IsBestSeller = product.IsBestSeller,
                    IsFreeShip = product.IsFreeShip
                };

                context.Products.Add(newProduct);
                await context.SaveChangesAsync();

                return RedirectToAction("AddProduct", "Admin");
            }

            var categories = new Category(context).GetAllCategories();

            ViewBag.categories = categories;

            return View(product);
        }

        // Product - DELETE
        [Route("admin/manage-products/delete/${id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await context.Products.FindAsync(id);
            context.Products.Remove(product);
            await context.SaveChangesAsync();

            return RedirectToAction("Products", "Admin");
        }
    }
}
