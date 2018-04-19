using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using Microsoft.AspNetCore.Identity;
using Library.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private ApplicationDbContext dbc = new ApplicationDbContext();
        
        public HomeController(UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            List<LibraryModel.Lease> Leases = new List<LibraryModel.Lease>();
            try{
                if (_signInManager.IsSignedIn(User)){
                    if((await _userManager.GetUsersInRoleAsync("Admin")).Contains(await _userManager.FindByNameAsync(_userManager.GetUserName(User)))){
                        List<LibraryModel.Lease> _leases = dbc.Lease.Include(l => l.Book).ToList();
                        Leases.AddRange(_leases);
                    }else{
                        ApplicationUser auser = await _userManager.FindByNameAsync(_userManager.GetUserName(User));
                        List<LibraryModel.Lease> _leases = dbc.Lease.Where(l => l.Email == auser.Email).Include(l => l.Book).ToList();
                        Leases.AddRange(_leases);
                    }
                } else {
                
                }
            }catch(Exception ex){

            }
            return View(Leases);
        }

        public IActionResult Catalog()
        {
            return View();
        }

        public IActionResult Result(string search)
        {
            search = search.ToLower();
            var punctuation = search.Where(Char.IsPunctuation).Distinct().ToArray();
            var words = search.Split().Select(x => x.Trim(punctuation));

            var results = dbc.Book.Where(b => words.Any(w => b.Title.ToLower().Contains(w)) || words.Any(w => b.Author.ToLower().Contains(w)) || words.Any(w => b.ISBN.ToLower().Contains(w)) || words.Any(w => b.Publisher.ToLower().Contains(w))).AsEnumerable();

            return View(results);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
