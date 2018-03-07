using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jetstreamsgo.Data;
using Microsoft.AspNetCore.Mvc;
using static jetstreamsgo.Models.LibraryModel;

namespace jetstreamsgo.Controllers
{
    public class BookController : Controller
    {
        private LibraryContext dbc = new LibraryContext();

        public IActionResult Add()
        {
            return View();
        }
        
        public IActionResult Index()
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
                Publisher = newBookPublisher,
            };
            dbc.Book.Add(newBook);
            dbc.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
