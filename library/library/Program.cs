using System;
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
        public static UserService userService = new UserService();

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

            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }

        public static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Please select from the following options:\n");
            Console.WriteLine("(1) Lend a book");
            Console.WriteLine("(2) Return a book");
            Console.WriteLine("(3) Add a new book");
            Console.WriteLine("(4) Search for books");
            Console.WriteLine("(5) Edit book(s)");
            Console.WriteLine("(6) Delete book(s)");
            Console.WriteLine("(7) List all books");
            Console.WriteLine("(8) Borrower History");
            Console.WriteLine("(9) Exit");
            Console.Write("\r\nPlease select an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    LendABook();
                    return true;
                case "2":
                    ReturnABook();
                    return true;
                case "3":
                    AddNewBook();
                    return true;
                case "4":
                    SearchBooks();
                    return true;
                case "5":
                    EditBook();
                    return true;
                case "6":
                    DeleteBook();
                    return true;
                case "7":
                    ListAllBooks();
                    return true;
                case "8":
                    BorrowerHistory();
                    return true;
                case "9":
                    return false;
                default:
                    return true;
            }
        }

        public static void LendABook()
        {
            var endLending = true;
            while (endLending)
            {
                Console.Clear();
                Console.WriteLine("Who would you like to lend to? Enter a name:");
                string borrower = Console.ReadLine();
                var borrowerResult = userService.SearchBorrowers(borrower);

                if (borrowerResult.Count == 0)
                {
                    Console.WriteLine("No results found.");
                    Console.ReadLine();
                }
                else
                {
                    var currentBorrower = borrowerResult[0];
                    Console.WriteLine("Enter a keyword for a book to lend: ");
                    var bookSearch = Console.ReadLine();
                    var bookSearchResults = bookService.SearchBooks(bookSearch);

                    if (bookSearchResults.Count == 0)
                    {
                        Console.WriteLine("No results found.");
                    }
                    else
                    {
                        foreach (Book book in bookSearchResults)
                        {
                            Console.WriteLine($"Lend {book.title} book to {currentBorrower.name}?(y/n)");
                            string lendingBook = Console.ReadLine();
                            var bookToLend = book;
                            if (lendingBook == "y")
                            {
                                userService.CreateLend(bookToLend, currentBorrower);
                            }
                        }
                    }
                }
                Console.WriteLine("Would you like to lend another book(y/n)?");
                if (Console.ReadLine() != "y")
                {
                    endLending = false;
                }
            }
        }

        public static void ReturnABook()
        {
            Console.Clear();
            var endReturns = true;

            while (endReturns)
            {
                Console.WriteLine("Enter a name of the person who is returning a book: ");
                var borrowerSearch = Console.ReadLine();
                var borrowers = userService.SearchBorrowers(borrowerSearch);
                if (borrowers.Count == 0)
                {
                    Console.WriteLine("No Results found");
                }
                else
                {
                    foreach (var borrower in borrowers)
                    {
                        Console.WriteLine($"Would you like to return a book from {borrower.name}?(y/n)");
                        var returningBook = Console.ReadLine();
                        if (returningBook == "y")
                        {
                            var borrowedBooks = userService.GetLendHistory(borrower.ID);
                            if (borrowers.Count == 0)
                            {
                                Console.WriteLine($"{borrower.name} currently has no books");
                            }
                            else
                            {
                                foreach (var bookhist in borrowedBooks)
                                {
                                    var allBooks = bookService.GetAllBooks();
                                    foreach(var book in allBooks)
                                    {                                      
                                        if (book.bookID == bookhist.bookID)
                                        {
                                            PrintBookDetails(book);
                                            Console.WriteLine("Return this book?(y/n)");
                                            var returnThisBook = Console.ReadLine();
                                            if (returnThisBook == "y")
                                            {
                                                userService.ReturnABook(bookhist);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                Console.WriteLine("Return another book?(y/n)");
                var returnAnotherBook = Console.ReadLine();
                if (returnAnotherBook != "y")
                {
                    endReturns = false;
                }
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

        //public static void SearchInBookList(string searchCriteria)
        //{
        //    bookService.SearchBooks(searchCriteria);
        //}

        public static void PrintBookDetails(Book book)
        {
            Console.WriteLine($"Title: {book.title} | Author: {book.author} | Series: {book.series} | Overall Rating: {book.overallRating}");
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
                    foreach (var book in editList)
                    {
                        PrintBookDetails(book);
                        Console.WriteLine("Would you like to edit this book? (y/n) ");
                        if (Console.ReadLine() == "y")
                        {
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
                                    book.title = Console.ReadLine();
                                }
                                else if (choice == "2")
                                {
                                    Console.WriteLine("Enter new Author: ");
                                    book.author = Console.ReadLine();
                                }
                                else if (choice == "3")
                                {
                                    Console.WriteLine("Enter new Series: ");
                                    book.series = Console.ReadLine();
                                }
                                else
                                {
                                    Console.WriteLine("Enter new Rating: ");
                                    int.TryParse (Console.ReadLine(), out book.overallRating);
                                }
                            }

                        bookService.EditBook(book);

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
            var books = bookService.GetAllBooks();

            foreach (var book in books)
            {
                PrintBookDetails(book);
            }
            Console.WriteLine("Press any key to return to main menu.");
            Console.ReadLine();
        }

        public static void BorrowerHistory()
        {
            Console.Clear();
            Console.WriteLine("Search for a borrower. Enter a name:");
            var borrower = Console.ReadLine();
            var borrowers = userService.GetBorrowers();
            var allBooks = bookService.GetAllBooks();

            foreach (var person in borrowers)
            {
                if (person.name == borrower)
                {
                    var borrowerHist = userService.GetLendHistory(person.ID);
                    foreach (var history in borrowerHist)
                    {
                        foreach (var book in allBooks)
                        {
                            if (book.bookID == history.bookID)
                            {
                                PrintBookDetails(book);
                                Console.WriteLine($"Date Borrowed: {history.dateOut.ToShortDateString()} | Date Returned: {history.dateIn.Value.ToShortDateString()}");
                            }
                        }
                    }
                }
            }
            Console.WriteLine("Press any key to return to main menu.");
            Console.ReadLine();
        }
    }
}

