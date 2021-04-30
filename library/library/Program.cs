using System;
using System.Collections.Generic;


namespace Learning_Projects
{
    class Program
    {
        private static List<Book> books = new List<Book>();
        
        static void Main(string[] args)
        {
            //add test books for testing only
            CreateTestBooks();
            //check for input

            //if search, then run search method
            //look up string contains method. https://docs.microsoft.com/en-us/dotnet/api/system.string.contains?view=net-5.0

            //if add, then run add method.

            //loop for input until quit

            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }

        private static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Please select from the following options:");
            Console.WriteLine("(1) Add a new book");
            Console.WriteLine("(2) Search for books");
            Console.WriteLine("(3) List all books");
            Console.WriteLine("(4) Exit");
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
                    ListAllBooks();
                    return true;
                case "4":
                    return false;
                default:
                    return true;
            }
        }

        private static void AddNewBook()
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

                Console.WriteLine("What rating would you give the book(1-10)? ");
                int.TryParse(Console.ReadLine(), out newBook.rating);
                while (newBook.rating < 1 || newBook.rating > 10)
                {
                    Console.WriteLine("The book must have an rating, please enter rating: ");
                    int.TryParse(Console.ReadLine(), out newBook.rating);
                }

                books.Add(newBook);

                newBook.ID = books.Count+1;

                Console.WriteLine("Would you like to add another book(y/n)?");
                if (Console.ReadLine() != "y")
                    endAdding = false;
            }
        }
        private static void SearchBooks()
        {
            //TODO later: Look at making the searching case insensitive (tip: look up string.tolower())

            var endSearching = true;

            //Create a list to store the books that match the search
            var bookResults = new List<Book>();

            while (endSearching)
            {
                bookResults.Clear();
                Console.Clear();
                Console.WriteLine("Enter a keyword to search: ");
                var bookSearch = Console.ReadLine();
                foreach (var book in books)
                {
                    if (book.title.Contains(bookSearch) || book.author.Contains(bookSearch))
                    {
                        //add the matching book to the result list
                        bookResults.Add(book);
                    }
                }

                //If the results list has items in it, loop through them and print them out
                if (bookResults.Count == 0)
                    Console.WriteLine("No results found.");
                else
                {
                    Console.WriteLine("Results found");
                    Console.WriteLine("Title      |     Author    |      Rating");
                    foreach (var book in bookResults)
                    {
                        PrintBookDetails(book);
                    }

                }

                Console.WriteLine("Would you like to search for another book(y/n)?");
                if (Console.ReadLine() != "y")
                    endSearching = false;
            }
        }

        private static void PrintBookDetails(Book book)
        {
            Console.WriteLine($"{book.title}, {book.author}, {book.rating}");
        }

        private static void CreateTestBooks()
        {
            books.Add(new Book { author = "Stephen King", rating = 4, title = "IT" });
        }

        private static void ListAllBooks()
        {
            foreach (var book in books)
            {
                PrintBookDetails(book);
            }

            Console.WriteLine("Press any key to return to main menu.");
            Console.ReadLine();
        }
    }

    class Book
    {
        public int ID;
        public string title;
        public string author;
        public int rating;
    }
}
