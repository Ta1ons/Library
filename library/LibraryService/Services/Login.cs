using LibraryService.Data;
using LibraryService.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LibraryService.Services
{
    public class LoginService
    {
        LibraryContext _context;

        public LoginService()
        {
            var dbAccessor = new DbAccessor();
            _context = dbAccessor.DataContext;
        }

        public bool EnterLogin(string login)
        {
            bool result = false;

            _context.Users.Where(x => x.Name.Contains(login));
            {
                result = true;
            }

            return result;
        }
    }
}
