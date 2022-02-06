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
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
