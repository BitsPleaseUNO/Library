using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Library.Data;
using Library.Models;
using static Library.Models.LibraryModel;

namespace Library.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BookController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext dbc = new ApplicationDbContext();
        
        public BookController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Add()
        {
            return View();
        }
        
        public IActionResult List()
        {
            IEnumerable<Book> _viewModel;
            _viewModel = dbc.Book.AsEnumerable();
            return View(_viewModel);
        }

        [HttpPost]
        public IActionResult Add(string isbn, string title, string author, int pages, string publisher)
        { 
            Book newBook = new Book
            {
                ISBN = isbn,
                Title = title,
                Author = author,
                Pages = pages,
                Language = "English",
                Publisher = publisher,
            };
            dbc.Book.Add(newBook);
            dbc.SaveChanges();

            return RedirectToAction("List", "Book");
        }

        public IActionResult Lease(string Id)
        {
            Book _viewModel = dbc.Book.FirstOrDefault(b => b.ISBN == Id);
            return View(_viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Lease(DateTime duedate, string email, string isbn)
        {
            Lease newLease;
            Book leasedBook = dbc.Book.First(b => b.ISBN == isbn);
            ApplicationUser leasingUser = await _userManager.FindByEmailAsync(email);
            if (dbc.Lease.Where(l => l.Book == leasedBook && l.LeaseStartDate < DateTime.Today && l.LeaseEndDate > DateTime.Now).Count() < 1)
            {
                newLease = new Lease
                {
                    LeaseStartDate = DateTime.Today,
                    LeaseEndDate = duedate,
                    Book = leasedBook,
                    Email = leasingUser.Email
                };
                dbc.Lease.Add(newLease);
                dbc.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
