using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Library.Data;
using Microsoft.AspNetCore.Mvc;
using static Library.Models.LibraryModel;

namespace Library.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BookController : Controller
    {
        private ApplicationDbContext dbc = new ApplicationDbContext();

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
    }
}
