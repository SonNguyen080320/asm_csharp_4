using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asm_rewrite.Models
{
    public class Alert
    {
        public string Message { get; set; }
        public string Type { get; set; }

        public Alert(string message, string type)
        {
            Message = message;
            Type = type;
        }
    }
}
