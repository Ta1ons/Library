using Library.Models;
using LibraryService.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraryTest
{
    [TestClass]
    public class UnitTest1 : TestBase
    {
        [TestMethod]
        public void AddBook()
        {
            //Arrange
            InitilizeDb();
            //var bookService = new BookService();
            //var newBook = new Book { author = "Matt" };

            //Act
            //bookService.AddBook(newBook);

            //Assert
            //Assert.IsTrue(bookService.GetAllBooks().Count == 12);
        }
    }
}
