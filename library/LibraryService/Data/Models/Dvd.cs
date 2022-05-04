using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryService.Data.Models
{
    public class Dvd
    {
        public int DvdID { get; set; }
        public string Title { get; set; }
        public string? Series { get; set; }
        public int? Year { get; set; }
        public string Genre { get; set; }
        public int? SeriesNo { get; set; }
    }
}
