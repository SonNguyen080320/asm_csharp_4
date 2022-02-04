using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asm_rewrite.Models
{
    public class Category
    {
        private readonly AsmContext context;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }

        public Category(AsmContext context)
        {
            this.context = context;
        }

        public List<Category> GetAllCategories()
        {
            return context.Categories.ToList();
        }
    }
}
