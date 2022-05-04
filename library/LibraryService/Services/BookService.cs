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

        public void SaveBook(Book bookToSave)
        {

            if (bookToSave.BookId > 0)
            {
                var book = _context.Books.FirstOrDefault(x => x.BookId == bookToSave.BookId);
                if (book != null) 
                {
                    book.Title = bookToSave.Title;
                    book.Author = bookToSave.Author;
                    book.Series = bookToSave.Series;
                    book.SeriesNo = bookToSave.SeriesNo;
                    _context.Update(book);
                }
            }
            else
            {
                Book book = new Book();

                book.Title = bookToSave.Title;
                book.Author = bookToSave.Author;
                book.Series = bookToSave.Series;
                book.SeriesNo = bookToSave.SeriesNo;
                _context.Add(book);
            }
            _context.SaveChanges();
        }

        public List<Book> SearchBooks(string searchCriteria)
        {
            return _context.Books.Where(b => 
            b.Title.Contains(searchCriteria) || 
            b.Author.Contains(searchCriteria) || 
            b.Series.Contains(searchCriteria) ).ToList();
        }

        public void DeleteBook(int bookID)
        {      
            var book = _context.Books.FirstOrDefault(x => x.BookId == bookID);
            if (book != null)
            {
                _context.Remove(book);
                _context.SaveChanges();
            }
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

