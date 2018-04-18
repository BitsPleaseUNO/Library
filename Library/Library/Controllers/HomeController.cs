using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using Microsoft.AspNetCore.Identity;
using Library.Data;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext dbc = new ApplicationDbContext();

        public IActionResult Index()
        {
            var user = this.User;
            
            System.Diagnostics.Debug.WriteLine(user.Identity.Name);
            System.Diagnostics.Debug.WriteLine(user.Identity.Name);
            System.Diagnostics.Debug.WriteLine(user.Identity.Name);
            System.Diagnostics.Debug.WriteLine(user.Identity.Name);
            System.Diagnostics.Debug.WriteLine(user.Identity.Name);
            System.Diagnostics.Debug.WriteLine(user.Identity.Name);
            System.Diagnostics.Debug.WriteLine(user.Identity.Name);
            System.Diagnostics.Debug.WriteLine(user.Identity.Name);
            System.Diagnostics.Debug.WriteLine(user.Identity.Name);

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
