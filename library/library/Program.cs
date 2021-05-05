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
            Console.WriteLine("(3) Delete book(s)");
            Console.WriteLine("(4) List all books");
            Console.WriteLine("(5) Exit");
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

                Console.WriteLine("Is this book part of a series? If no, press enter. If yes, what is the name of the series: ");
                newBook.series = Console.ReadLine();

                Console.WriteLine("What rating would you give the book(1-10)? ");
                int.TryParse(Console.ReadLine(), out newBook.rating);
                while (newBook.rating < 1 || newBook.rating > 10)
                {
                    Console.WriteLine("The book must have an rating, please enter rating: ");
                    int.TryParse(Console.ReadLine(), out newBook.rating);
                }

                books.Add(newBook);

                newBook.ID = books.Count + 1;

                Console.WriteLine("Would you like to add another book(y/n)?");
                if (Console.ReadLine() != "y")
                    endAdding = false;
            }
        }
        private static void SearchBooks()
        {
            var endSearching = true;

            //Create a list to store the books that match the search
            var searchResults = new List<Book>();

            while (endSearching)
            {
                searchResults.Clear();
                Console.Clear();
                Console.WriteLine("Enter a keyword to search: ");
                var bookSearch = Console.ReadLine();

                SearchInBookList(searchResults, bookSearch);

                //If the results list has items in it, loop through them and print them out
                if (searchResults.Count == 0)
                    Console.WriteLine("No results found.");
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
                    endSearching = false;
            }
        }

        private static void SearchInBookList(List<Book> listToAddResultsTo, string searchCriteria)
        {
            foreach (var book in books)
            {
                if (book.title.Contains(searchCriteria, StringComparison.OrdinalIgnoreCase) || book.author.Contains(searchCriteria, StringComparison.OrdinalIgnoreCase) || book.series.Contains(searchCriteria, StringComparison.OrdinalIgnoreCase))
                {
                    //add the matching book to the result list
                    listToAddResultsTo.Add(book);
                }
            }
        }

        private static void PrintBookDetails(Book book)
        {
            Console.WriteLine("|     ID     |     Title     |     Author     |     Series     |     Rating     |");
            Console.WriteLine($"{book.ID}, {book.title}, {book.author}, {book.series}, {book.rating}");
        }

        private static void CreateTestBooks()
        {
            books.Add(new Book { author = "Stephen King", rating = 4, title = "IT", ID = 1 });
        }

        private static void DeleteBook()
        {
            var removeList = new List<Book>();
            var endSearch = true;

            while (endSearch)
            {
                removeList.Clear();
                Console.Clear();
                Console.WriteLine("Enter a book to delete: ");
                var bookSearch = Console.ReadLine();

                SearchInBookList(removeList, bookSearch);

                if (removeList.Count == 0)
                    Console.WriteLine("No results found.");
                else
                {
                    Console.WriteLine("Results found!");
                    foreach (var book in removeList)
                    {
                        PrintBookDetails(book);
                    }

                    Console.WriteLine("If the book you are looking for isn't in the list, please press enter. Otherwise, please select an ID to delete: ");
                    var deleteBookID = int.Parse(Console.ReadLine());
                    //if (deleteBookID == book.ID)
                    //{
                    books.Remove(new Book() {ID = deleteBookID });
                    //}
                    //else
                    //    endSearch = false;
                }
            }
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
        public string series;
        public int rating;
    }
}
