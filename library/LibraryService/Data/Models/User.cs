using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryService.Data.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsBorrower { get; set; }

        public User() { }
        public User(string aname)
        {
            Name = aname;
        }
    }
}
