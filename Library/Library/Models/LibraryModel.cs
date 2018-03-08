using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class LibraryModel
    {
        public class Book
        {
            public string ISBN { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public string Language { get; set; }
            public int Pages { get; set; }
            public string Publisher { get; set; }
        }
    }
}
