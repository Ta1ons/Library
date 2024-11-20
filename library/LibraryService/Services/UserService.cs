using LibraryService.Data;
using LibraryService.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


namespace LibraryService.Services
{
    public class UserService
    {
        LibraryContext _context;
        public UserService()
        {
            var dbAccessor = new DbAccessor();
            _context = dbAccessor.DataContext;
        }

        //private string borrowerspath = @"C:\Gitprojects\Library\library\library\Data\Borrowers.txt";
        //private string historypath = @"C:\Gitprojects\Library\library\library\Data\BookHistory.txt";
        

        public List<User> SearchForBorrowers(string searchCriteria)
        {
            return _context.Users.Where(x => x.Name.Equals(searchCriteria)).ToList();
        }

        public void CreateLend(Book book, User borrower)
        {
            BookHistory bookHistory = new BookHistory();

            bookHistory.BookId = book.BookId;
            bookHistory.UserId = borrower.UserId;
            bookHistory.DateOut = DateTime.Now;

            _context.Add(bookHistory);
            _context.SaveChanges();
        }

        public List<BookHistory> GetLendHistory(int userID) //.Include(x=>x.Borrower).Include(x=>x.Book)
        {
            var historyList = GetLendHistoryForBorrower(userID);
            List<BookHistory> lendersHistory = new List<BookHistory>();

            foreach (BookHistory history in historyList)
            {
                if (history.UserId == userID)
                {
                    lendersHistory.Add(history);
                }
            }
            return lendersHistory;
        }

        public List<BookHistory> GetLendHistoryForBorrower(int userId)
        {
            return _context.BookHistory.Include(x=>x.Book).Where(bookHistory => bookHistory.UserId == userId).ToList(); 
        }

        public void ReturnABook(BookHistory borrowedbook)
        {
            var bookHistory = _context.BookHistory.FirstOrDefault(x => x.BookHistoryId == borrowedbook.BookHistoryId);

            bookHistory.DateIn = DateTime.Now;

            _context.Update(bookHistory);
            _context.SaveChanges();
        }

        public List<User> GetAllBorrowers()
        {
            return _context.Users.Take(10).ToList();
        }

        public List<User> GetBorrowersFromHistory(List<BookHistory> bookHistories) 
        {
            var users = new List<User>();

            foreach (var history in bookHistories)
            {
                var user = _context.Users.FirstOrDefault(x => x.UserId == history.UserId);
                users.Add(user);
            }
            return users;
        }
    }
}
