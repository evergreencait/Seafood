using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Seafood.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Seafood.Controllers
{
    public class MailingController : Controller
    {
        // GET: /<controller>/
        private ApplicationDbContext db = new ApplicationDbContext();
        public IActionResult Index()
        {
            return View(db.Mailings.ToList());
        }
    }
}