using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asm_rewrite.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string FistName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
