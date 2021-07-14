﻿using Library.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Library.Services
{
    public class BookService
    {
        //private List<Book> books = new List<Book>();
        private string path = @"C:\Gitprojects\Library\library\library\Data\BookData.txt";

        public void AddBook(Book bookToAdd)
        {
            string[] lines = File.ReadAllLines(path);
            int BookID = 0;

            foreach (string line in lines)
            {
                string[] stringToList = line.Split(",");
                int.TryParse(stringToList[0], out int number);
                if ( number > BookID) //make sure this is the largest number
                {
                    BookID = number;
                }
            }

            if (bookToAdd.bookID == 0)
            {
                bookToAdd.bookID = BookID + 1;
            }            

            var bookString = ConvertBookToString(bookToAdd);

            if (!File.Exists(path))
            {
                File.Create(path);
            }

            using StreamWriter sw = File.AppendText(path);
            {
                sw.WriteLine(bookString);
            }
        }

        public List<Book> SearchBooks(string searchCriteria)
        {
            var returnList = new List<Book>();
            //var sBooks = returnList.Where(b => b.author.Contains("Matt")).ToList();
            
            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                var book = ConvertStringToBook(line);
                
                if (book.title.Contains(searchCriteria, StringComparison.OrdinalIgnoreCase) || book.author.Contains(searchCriteria, StringComparison.OrdinalIgnoreCase) || book.series.Contains(searchCriteria, StringComparison.OrdinalIgnoreCase))
                {
                    returnList.Add(book);
                }
            }
            return returnList;
        }

        public void DeleteBook(Book bookToDelete)
        {
            var bookList = GetAllBooks();

            foreach ( Book book in bookList)
                if ( bookToDelete.bookID == book.bookID)
                {
                    bookToDelete = book;
                }

            bookList.Remove(bookToDelete);

            File.Create(path).Close();

            foreach (Book book in bookList)
            {
                AddBook(book);
            }
        }

        public void EditBook(Book updatedBook)
        {
            var bookList = GetAllBooks();
            Book bookToDelete = new Book();

            foreach (Book book in bookList)
                if (updatedBook.bookID == book.bookID)
                {
                    bookToDelete = book;
                }

            bookList.Remove(bookToDelete);
            bookList.Add(updatedBook);

            File.Create(path).Close();

            foreach (Book item in bookList)
            {
                AddBook(item);
            }
        }

        public List<Book> GetAllBooks()
        {
            var returnList = new List<Book>();
            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                var book = ConvertStringToBook(line);
                returnList.Add(book);
            }

            return returnList;
        }

        private String ConvertBookToString(Book bookToAdd)
        {
            return String.Join(',', new string[] { bookToAdd.bookID.ToString(), bookToAdd.title, bookToAdd.author, bookToAdd.series, bookToAdd.overallRating.ToString() });
        }

        private Book ConvertStringToBook(string line)
        {
            string[] stringToList = line.Split(",");

            Book book = new Book();

            int.TryParse(stringToList[0], out book.bookID);
            book.title = stringToList[1];
            book.author = stringToList[2];
            book.series = stringToList[3];
            int.TryParse(stringToList[4], out book.overallRating);

            return book;
        }
    }
}

