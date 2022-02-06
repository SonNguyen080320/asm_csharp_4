using asm_rewrite.Models;
using asm_rewrite.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asm_rewrite.Controllers
{
    public class CartController : Controller
    {
        private readonly AsmContext context;
        public CartController(AsmContext context)
        {
            this.context = context;
        }

        [Route("cart")]
        public IActionResult Index()
        {
            var categories = context.Categories.ToList();
            ViewBag.categories = categories;

            var cart = SessionExtensions.GetSessionData<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart != null ? cart.Sum(item => item.Product.HotPrice !=null ? item.Product.HotPrice * item.Quantity : item.Product.Price * item.Quantity) : 0;

            return View();
        }

        private int IsExist(int id)
        {
            List<Item> cart = SessionExtensions.GetSessionData<List<Item>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.Id.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }

        [Route("buy/{id}")]
        public IActionResult Buy(int id)
        {
            if (SessionExtensions.GetSessionData<List<Product>>(HttpContext.Session, "cart") == null)
            {
                var newItem = new Item { Product = context.Products.Single(p => p.Id == id), Quantity = 1 };

                List<Item> cart = new List<Item>();
                cart.Add(newItem);
                SessionExtensions.SetSessionData(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Item> cart = SessionExtensions.GetSessionData<List<Item>>(HttpContext.Session, "cart");
                int index = IsExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    var newItem = new Item { Product = context.Products.Single(p => p.Id == id), Quantity = 1 };
                    cart.Add(newItem);
                }
                SessionExtensions.SetSessionData(HttpContext.Session, "cart", cart);
            }

            return RedirectToAction("index");
        }

        [Route("remove/{id}")]
        public IActionResult Remove(int id)
        {
            List<Item> cart = SessionExtensions.GetSessionData<List<Item>>(HttpContext.Session, "cart");
            int index = IsExist(id);
            cart.RemoveAt(index);
            SessionExtensions.SetSessionData(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        [Route("decrease/{id}")]
        public IActionResult Decrease(int id)
        {
            List<Item> cart = SessionExtensions.GetSessionData<List<Item>>(HttpContext.Session, "cart");
           
            int index = IsExist(id);

            if (cart[index].Quantity > 1)
            {
                cart[index].Quantity--;
                SessionExtensions.SetSessionData(HttpContext.Session, "cart", cart);
            }

            return RedirectToAction("index");
        }
    }
}
