using Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services
{
    public class LoginService
    {
        List<User> users = new List<User>() { new User(1, "Adam"), new User(2, "Larissa") };

        private User currentUser = new User();

        //current user us set from login and unable to be set outside of class.
        public User CurrentUser
        {
            get { return currentUser; }
        }

        public bool EnterLogin(string login)
        {
            bool result = false;
            foreach (var user in users)
                if (user.name.Contains(login, StringComparison.OrdinalIgnoreCase))
                {
                    result = true;
                    currentUser = user;
                }

            return result;
        }

        public string GetCurrentUser(string myUser)
        {
            string CurrentUser = "";

            foreach (var user in users)
                if (user.name.Contains(myUser, StringComparison.OrdinalIgnoreCase))
                {
                    CurrentUser = user.name;
                }
            return CurrentUser;
        }
    }
}
