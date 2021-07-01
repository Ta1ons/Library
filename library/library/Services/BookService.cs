using Library.Models;
using System;
using System.Collections.Generic;

namespace Library.Services
{
    public class BookService
    {
        private List<Book> books = new List<Book>();

        public void AddBook(Book bookToAdd)
        {
            bookToAdd.ID = books.Count + 1;
            books.Add(bookToAdd);
        }

        public List<Book> SearchBooks(string searchCriteria)
        {
            var returnList = new List<Book>();
            foreach (var book in books)
            {
                if (book.title.Contains(searchCriteria, StringComparison.OrdinalIgnoreCase) || book.author.Contains(searchCriteria, StringComparison.OrdinalIgnoreCase) || book.series.Contains(searchCriteria, StringComparison.OrdinalIgnoreCase))
                {
                    //add the matching book to the result list
                    returnList.Add(book);
                }
            }
            return returnList;
        }

        public void DeleteBook(Book book)
        {
            books.Remove(book);
        }

        public List<Book> GetAllBooks()
        {
            return books;
        }

        //public List<Review> GetReviewsForBook()
        //{
            
        //}
    }
}
