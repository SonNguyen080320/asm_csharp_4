﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace asm_rewrite.Models
{
    public class Product
    {
        private readonly AsmContext context;

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public double ImportPrice { get; set; }
        public double Price { get; set; }
        public double? HotPrice { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get; set; }
        public int Quantity { get; set; }
        public bool IsTop { get; set; }
        public bool IsBestSeller { get; set; }
        public bool IsFreeShip { get; set; }

        public Product(AsmContext context)
        {
            this.context = context;
        }

        public List<Product> GetAllProducts()
        {
            return context.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            return context.Products.Single(p => p.Id == id);
        }

        public Product GetCurrentProduct(string alias)
        {
            return context.Products.Single(p => p.Alias == alias);
        }

        public List<Product> GetTopProducts()
        {
            return context.Products.Where(p => p.IsTop).ToList();
        }

        public List<Product> GetBestSellerProducts()
        {
            return context.Products.Where(p => p.IsBestSeller).ToList();
        }
    }
}