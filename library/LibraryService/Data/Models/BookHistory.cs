﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryService.Data.Models
{
    public class BookHistory
    {
        public int BookHistoryId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime DateOut { get; set; }
        public DateTime? DateIn { get; set; }
        public virtual Book Book { get; set; }
        public virtual User User { get; set; }
    }
}
