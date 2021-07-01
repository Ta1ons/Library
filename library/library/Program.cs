using System;
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
            CreateTestBooks();

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
            Console.WriteLine("(3) Delete book(s)");
            Console.WriteLine("(4) List all books");
            Console.WriteLine("(5) User profile");
            Console.WriteLine("(6) Exit");
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
                    DeleteBook();
                    return true;
                case "4":
                    ListAllBooks();
                    return true;
                case "5":
                    UserDetails();
                    return true;
                case "6":
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
                //Create a list to store the books that match the search
                var searchResults = new List<Book>();
                Console.Clear();
                Console.WriteLine("Enter a keyword to search: ");
                var bookSearch = Console.ReadLine();

                searchResults = bookService.SearchBooks(bookSearch);


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

        public static void SearchInBookList(List<Book> listToAddResultsTo, string searchCriteria)
        {
            bookService.SearchBooks(searchCriteria);
        }

        public static void PrintBookDetails(Book book)
        {
            Console.WriteLine($"{book.ID}, {book.title}, {book.author}, {book.series}, {book.overallRating}");
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
                    Console.WriteLine("Would you like to search for another book(y/n)?");
                    if (Console.ReadLine() != "y")
                    {
                        endSearch = false;
                    }
                }
            }
        }
        public static void ListAllBooks()
        {
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

