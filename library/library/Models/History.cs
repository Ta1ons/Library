using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models
{
    class History
    {
        public int historyID;
        public int userID;
        public int bookID;
        public DateTime dateOut;
        public DateTime? dateIn;
    }
}
