using LibraryService.Data;
using LibraryService.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LibraryService.Services
{
    public class BookService
    {
        LibraryContext _context;

        public BookService()
        {
            var dbAccessor = new DbAccessor();
            _context = dbAccessor.DataContext;
        }

        //private List<Book> books = new List<Book>();
        //private string path = @"C:\Gitprojects\Library\library\library\Data\BookData.txt";


        public void AddBook(Book bookToAdd)
        {
            Book book = new Book();

            book.Title = bookToAdd.Title;
            book.Author = bookToAdd.Author;
            book.Series = bookToAdd.Series;

            _context.Add(book);
            _context.SaveChanges();
        }

        public List<Book> SearchBooks(string searchCriteria)
        {
            return _context.Books.Where(b => 
            b.Title.Contains(searchCriteria) || 
            b.Author.Contains(searchCriteria) || 
            b.Series.Contains(searchCriteria) ).ToList();
        }

        public void DeleteBook(Book bookToDelete)
        {
            _context.Remove(bookToDelete);
            _context.SaveChanges();
        }

        public void UpdateBook(Book updatedBook)
        {
            _context.Update(updatedBook);
            _context.SaveChanges();
        }

        public List<Book> GetAllBooks()
        {
            return _context.Books.Take(200).ToList();
        }
    }
}

