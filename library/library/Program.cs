using System;
using System.Collections.Generic;


namespace Learning_Projects
{
    class Program
    {
        private static List<Book> books = new List<Book>();
        static void Main(string[] args)
        {
            //check for input

            //if search, then run search method
            //look up string contains method. https://docs.microsoft.com/en-us/dotnet/api/system.string.contains?view=net-5.0

            //if add, then run add method.

            //loop for input until quit
            var adding = true;

            

            Console.WriteLine("Please select from the following options:" +
                "\nAdd a new book (1)" +
                "\nSearch for a book (2)" +
                "\nQuit (3)");
            var menuSelection = Console.ReadLine();

            if (menuSelection == "1")

            while (adding)
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
                //newBook.rating = int.Parse(Console.ReadLine());
                while (newBook.rating < 1 || newBook.rating > 10)
                {
                    Console.WriteLine("The book must have an rating, please enter rating: ");
                    int.TryParse(Console.ReadLine(), out newBook.rating);
                    //newBook.rating = int.Parse(Console.ReadLine());
                }

                books.Add(newBook);
                Book.Count++;

                Console.WriteLine("Would you like to add another book(y/n)?");
                if (Console.ReadLine() != "y")
                    adding = false;
            }

            if (menuSelection == "2")
            Console.WriteLine("Enter a keyword: ");
            var bookSearch = Console.ReadLine();
            foreach (var book in books)
            {      
                if (book.title.Contains(bookSearch)||book.author.Contains(bookSearch))
                {
                    var result = book;
                    Console.WriteLine(result);
                }
            }

            if (menuSelection == "3")
                Environment.Exit(0);
        }
    }

    class Book
    {
        static public int Count=0;
        public string title;
        public string author;
        public int rating;
    }
}
