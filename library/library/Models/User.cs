using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models
{
    public class User
    {
        public int personalID;
        public string name;

        public User() { }
        public User(int apersonalID, string aname)
        {
            personalID = apersonalID;
            name = aname;
        }
    }

    public class Borrower
    {
        public int ID;
        public string name;
    }
}
