using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Data.Models
{
    public class BookHistory
    {


        public virtual Book Book { get; set; }
        public virtual User Borrower { get; set; }
    }
}
