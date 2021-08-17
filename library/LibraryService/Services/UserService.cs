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

            bookHistory.Book = book;
            bookHistory.Borrower = borrower;
            bookHistory.DateOut = DateTime.Now;

            _context.Add(bookHistory);
            _context.SaveChanges();
        }

        public List<BookHistory> GetLendHistory(int userID)
        {
            var historyList = GetLendHistoryForBorrower(userID);
            List<BookHistory> lendersHistory = new List<BookHistory>();

            foreach (BookHistory history in historyList)
            {
                if (history.Borrower.UserId == userID)
                {
                    lendersHistory.Add(history);
                }
            }
            return lendersHistory;
        }

        public List<BookHistory> GetLendHistoryForBorrower(int userId)
        {
            return _context.BookHistories.Where(bookHistory => bookHistory.Borrower.UserId == userId).ToList(); //.Include(x=>x.Borrower).Include(x=>x.Book)
        }

        public void ReturnABook(BookHistory book)
        {
            var bookHistory = _context.BookHistories.FirstOrDefault(x => x.BookHistoryId == book.BookHistoryId);

            bookHistory.DateIn = DateTime.Now;

            _context.Update(bookHistory);
            _context.SaveChanges();
        }
    }
}
