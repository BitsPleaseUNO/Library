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
            public virtual ICollection<Lease> Leases { get; set; }
        }

        public class Lease
        {
            public int Id { get; set; }
            public virtual Book Book { get; set; }
            public string Email { get; set; }

            public DateTime LeaseStartDate { get; set; }

            public DateTime LeaseEndDate { get; set; }
        }
    }
}
