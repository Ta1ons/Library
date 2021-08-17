using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryService.Data.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Series { get; set; }
    }
}
