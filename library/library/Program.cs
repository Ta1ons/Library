﻿using System;
using System.IO;
using System.Collections.Generic;
using Library.Models;
using Library.Services;

namespace Library
{
    class Program
    {

        public static BookService bookService = new BookService();
        public static LoginService login = new LoginService();

        static void Main(string[] args)
        {
            Console.WriteLine("Enter Username: ");
            string userName = Console.ReadLine();
            var authenticated = login.EnterLogin(userName);
            while (authenticated == false)
            {
                Console.WriteLine("Try Again");
                Console.WriteLine("Enter Username: ");
                userName = Console.ReadLine();
                authenticated = login.EnterLogin(userName);
            }

            //add test books for testing only
            //CreateTestBooks();

            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }

        public static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Please select from the following options:");
            Console.WriteLine("(1) Add a new book");
            Console.WriteLine("(2) Search for books");
            Console.WriteLine("(3) Edit book(s)");
            Console.WriteLine("(4) Delete book(s)");
            Console.WriteLine("(5) List all books");
            Console.WriteLine("(6) User profile");
            Console.WriteLine("(7) Exit");
            Console.Write("\r\nPlease select an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    AddNewBook();
                    return true;
                case "2":
                    SearchBooks();
                    return true;
                case "3":
                    EditBook();
                    return true;
                case "4":
                    DeleteBook();
                    return true;
                case "5":
                    ListAllBooks();
                    return true;
                case "6":
                    UserDetails();
                    return true;
                case "7":
                    return false;
                default:
                    return true;
            }
        }

        public static void AddNewBook()
        {
            var endAdding = true;

            Console.Clear();

            while (endAdding)
            {
                var newBook = new Book();

                Console.WriteLine("What is the title of the book: ");
                newBook.title = Console.ReadLine();
                while (newBook.title == "")
                {
                    Console.WriteLine("The book must have an title, please enter the title: ");
                    newBook.title = Console.ReadLine();
                }

                Console.WriteLine("Who is the author of the book: ");
                newBook.author = Console.ReadLine();
                while (newBook.author == "")
                {
                    Console.WriteLine("The book must have an author, please enter an author: ");
                    newBook.author = Console.ReadLine();
                }

                Console.WriteLine("Is this book part of a series? (y/n)");
                if (Console.ReadLine() == "y")
                {
                    Console.WriteLine("Enter name of the series: ");
                    newBook.series = Console.ReadLine();
                }
                else
                {
                    newBook.series = "";
                }

                Console.WriteLine("What rating would you give the book(1-5)? ");
                int.TryParse(Console.ReadLine(), out newBook.overallRating);
                while (newBook.overallRating < 1 || newBook.overallRating > 5)
                {
                    Console.WriteLine("The book must have an rating, please enter rating: ");
                    int.TryParse(Console.ReadLine(), out newBook.overallRating);
                }

                bookService.AddBook(newBook);

                Console.WriteLine("Would you like to add another book(y/n)?");
                if (Console.ReadLine() != "y")
                {
                    endAdding = false;
                }
            }
        }

        public static void SearchBooks()
        {
            var endSearching = true;

            while (endSearching)
            {
                //var searchResults = new List<Book>();
                Console.Clear();
                Console.WriteLine("Enter a keyword to search: ");
                var bookSearch = Console.ReadLine();

                var searchResults = bookService.SearchBooks(bookSearch);


                //If the results list has items in it, loop through them and print them out
                if (searchResults.Count == 0)
                {
                    Console.WriteLine("No results found.");
                }
                else
                {
                    Console.WriteLine("Results found!");
                    Console.WriteLine("|     ID     |     Title     |     Author     |     Series     |     Rating     |");
                    foreach (var book in searchResults)
                    {
                        PrintBookDetails(book);
                    }

                }
                Console.WriteLine("Would you like to search for another book(y/n)?");
                if (Console.ReadLine() != "y")
                {
                    endSearching = false;
                }
            }
        }

        public static void SearchInBookList(string searchCriteria)
        {
            bookService.SearchBooks(searchCriteria);
        }

        public static void PrintBookDetails(Book book)
        {
            Console.WriteLine($"{book.ID}, {book.title}, {book.author}, {book.series}, {book.overallRating}");
        }

        public static void EditBook()
        {
            var editList = new List<Book>();
            var endSearch = true;

            while (endSearch)
            {
                editList.Clear();
                Console.Clear();
                Console.WriteLine("This option is to edit a book. Please enter a keyword to search: ");
                var bookSearch = Console.ReadLine();

                editList = bookService.SearchBooks(bookSearch);

                if (editList.Count == 0)
                {
                    Console.WriteLine("No results found.");
                }
                else
                {
                    Console.WriteLine("Results found!");
                    Console.WriteLine("|     ID     |     Title     |     Author     |     Series     |     Rating     |");
                    foreach (var book in editList)
                    {
                        PrintBookDetails(book);
                        Console.WriteLine("Would you like to edit this book? (y/n) ");
                        if (Console.ReadLine() == "y")
                        {
                            var updatedBook = book;
                            Console.WriteLine("Please select the option you wish to Edit:");
                            Console.WriteLine("1) Title");
                            Console.WriteLine("2) Author");
                            Console.WriteLine("3) Series");
                            Console.WriteLine("4) Rating");
                            var choice = Console.ReadLine();
                            {
                                if (choice == "1")
                                {
                                    Console.WriteLine("Enter new Title: ");
                                    updatedBook.title = Console.ReadLine();
                                }
                                else if (choice == "2")
                                {
                                    Console.WriteLine("Enter new Author: ");
                                    updatedBook.author = Console.ReadLine();
                                }
                                else if (choice == "3")
                                {
                                    Console.WriteLine("Enter new Series: ");
                                    updatedBook.series = Console.ReadLine();
                                }
                                else
                                {
                                    Console.WriteLine("Enter new Rating: ");
                                    int.TryParse (Console.ReadLine(), out updatedBook.overallRating);
                                }
                            }

                            bookService.EditBook(updatedBook, book);

                            Console.WriteLine("Book updated!");
                        }
                    }
                }
                Console.WriteLine("Would you like to search for another book(y/n)?");
                if (Console.ReadLine() != "y")
                {
                    endSearch = false;
                }
            }
        }
        public static void DeleteBook()
        {
            var removeList = new List<Book>();
            var endSearch = true;

            while (endSearch)
            {
                removeList.Clear();
                Console.Clear();
                Console.WriteLine("This option is to delete a book. Please enter a keyword to search: ");
                var bookSearch = Console.ReadLine();

                removeList = bookService.SearchBooks(bookSearch);

                if (removeList.Count == 0)
                {
                    Console.WriteLine("No results found.");
                }
                else
                {
                    Console.WriteLine("Results found!");
                    Console.WriteLine("|     ID     |     Title     |     Author     |     Series     |     Rating     |");
                    foreach (var book in removeList)
                    {
                        PrintBookDetails(book);
                        Console.WriteLine("Would you like to delete this book? (y/n) ");
                        if (Console.ReadLine() == "y")
                        {
                            bookService.DeleteBook(book);

                            Console.WriteLine("Book Deleted!");
                        }
                    }
                }
                Console.WriteLine("Would you like to search for another book(y/n)?");
                if (Console.ReadLine() != "y")
                {
                    endSearch = false;
                }
            }
        }
        public static void ListAllBooks()
        {
            Console.Clear();
            Console.WriteLine("|     ID     |     Title     |     Author     |     Series     |     Rating     |");
            var books = bookService.GetAllBooks();

            foreach (var book in books)
            {
                PrintBookDetails(book);
            }
            Console.WriteLine("Press any key to return to main menu.");
            Console.ReadLine();
        }
        public static void CreateTestBooks()
        {
            bookService.AddBook(new Book { author = "Stephen King", overallRating = 4, title = "IT", ID = 1, series = "" });
        }

        public static void UserDetails()
        {
            Console.Clear();
            Console.WriteLine($"{login.CurrentUser.name}");
            Console.ReadLine();
                //bookService.GetReviewsForBook()
                //bookService.GetBorrowedHistory()
                //bookService.GetRatingsForBook()
        }
    }
}

