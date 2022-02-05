using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asm_rewrite.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace asm_rewrite.Controllers
{
    public class AdminController : Controller
    {
        private readonly AsmContext context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AdminController(AsmContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        [Route("admin")]
        public IActionResult Index()
        {
            return View();
        }

        // products - GET
        [Route("admin/manage-products")]
        [HttpGet]
        public IActionResult Products(string SearchText = "", int page = 1)
        {   
            List<Product> products = context.Products.ToList();
            //

            if (SearchText != null && SearchText != "")
            {
                products = products.Where(p => p.Name.ToLower().Contains(SearchText.ToLower())).ToList();
            } else
            {
                products = context.Products.ToList();
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
            var categories = new Category(context).GetAllCategories();

            ViewBag.categories = categories;

            return View();
        }

        //[Route("admin/manage-products/add")]
        //[HttpPost]
        //public async Task<IActionResult> AddProduct(
        //    [Bind("CategoryId, Name, Alias, Brand, ImportPrice, Price, HotPrice, Quantity, Description, Image, IsDeleted, IsTop, IsBestSeller, IsFreeShip, ImageFile")] Product product)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        string imgPath = UploadFile(product.ImageFile);

        //        var newProduct = new Product
        //        {
        //            Name = product.Name,
        //            CategoryId = product.CategoryId,
        //            Alias = product.Alias,
        //            Brand = product.Brand,
        //            ImportPrice = product.ImportPrice,
        //            Price = product.Price,
        //            HotPrice = product.HotPrice != null ? product.HotPrice : null,
        //            Quantity = product.Quantity,
        //            Description = product.Description,
        //            Image = imgPath,
        //            IsDeleted = product.IsDeleted,
        //            IsTop = product.IsTop,
        //            IsBestSeller = product.IsBestSeller,
        //            IsFreeShip = product.IsFreeShip
        //        };

        //        context.Products.Add(newProduct);
        //        await context.SaveChangesAsync();

        //        return RedirectToAction("AddProduct", "Admin");
        //    }

        //    var categories = new Category(context).GetAllCategories();

        //    ViewBag.categories = categories;

        //    return View(product);
        //}

        //private string UploadFile(IFormFile ImageFile)
        //{
        //    string uniqueFileName = null;

        //    if (ImageFile != null)
        //    {
        //        string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
        //        uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            ImageFile.CopyTo(fileStream);
        //        }
        //    }

        //    return uniqueFileName;
        //}

        //[Route("admin/manage-categories")]
        //public IActionResult Categories()
        //{
        //    return View();
        //}

        //[Route("admin/manage-customers")]
        //public IActionResult Customers()
        //{
        //    return View();
        //}

        //[Route("admin/manage-users")]
        //public IActionResult Users()
        //{
        //    return View();
        //}
    }
}
