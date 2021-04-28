using System;
using System.Collections.Generic;


namespace Learning_Projects
{
    class Program
    {
        static void Main(string[] args)
        {
            var books = new List<book>();

            var adding = true;

            while (adding)
            {
                var newBook = new book();

                Console.WriteLine("What is the title of the book: ");
                newBook.title = Console.ReadLine();

                Console.WriteLine("Who is the author of the book: ");
                newBook.author = Console.ReadLine();

                Console.WriteLine("What rating would you give the book(1-10)? ");
                newBook.rating = int.Parse(Console.ReadLine());

                books.Add(newBook);
                book.Count++;

                Console.WriteLine("Would you like to add another book(y/n)?");
                if (Console.ReadLine() != "y")
                    adding = false;
            }

            foreach (var newBook in books)
            {
                Console.WriteLine($"ID:{book.Count}. {newBook.title} by {newBook.author}. My raiting for this book is: {newBook.rating}");
            }
        }
    }

    class book
    {
        static public int Count;
        public string title;
        public string author;
        public int rating;
    }
}
//while (author == "")
//{
//    Console.WriteLine("The book must have an author, please enter author:");
//   author.Add(Console.ReadLine());