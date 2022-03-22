// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace LibraryServerApp.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Gitprojects\Library\library\LibraryServerApp\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Gitprojects\Library\library\LibraryServerApp\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Gitprojects\Library\library\LibraryServerApp\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Gitprojects\Library\library\LibraryServerApp\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Gitprojects\Library\library\LibraryServerApp\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Gitprojects\Library\library\LibraryServerApp\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Gitprojects\Library\library\LibraryServerApp\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Gitprojects\Library\library\LibraryServerApp\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Gitprojects\Library\library\LibraryServerApp\_Imports.razor"
using LibraryServerApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Gitprojects\Library\library\LibraryServerApp\_Imports.razor"
using LibraryServerApp.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Gitprojects\Library\library\LibraryServerApp\_Imports.razor"
using LibraryServerApp.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Gitprojects\Library\library\LibraryServerApp\Pages\Borrowing.razor"
using LibraryService.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Gitprojects\Library\library\LibraryServerApp\Pages\Borrowing.razor"
using LibraryService.Data.Models;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/Borrowing")]
    public partial class Borrowing : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 130 "C:\Gitprojects\Library\library\LibraryServerApp\Pages\Borrowing.razor"
       

    private bool Lending = false;
    private bool Returning = false;
    private UserService _userService;
    private BookService _bookService;
    private List<User> Users = new List<User>();
    private List<Book> Books = new List<Book>();
    private List<BookHistory> bookHistories = new List<BookHistory>();

    private BookHistory NewBookHistory;
    private string searchCriteria = string.Empty;

    private User Borrower;
    private Book wholebook;

    private string message;
    private bool DialogIsOpenBook = false;
    private bool DialogIsOpenHist = false;

    protected override async Task OnInitializedAsync()
    {
        _userService = new UserService();
        _bookService = new BookService();
        GetUsers();
    }

    public void GetUsers()
    {
        Users = _userService.GetAllBorrowers();
    }

    private void GetBooks()
    {
        Books = _bookService.GetAllBooks();
    }

    private void NotLending()
    {
        Lending = false;
        Returning = false;
    }

    private void LendABook(User user)
    {
        GetBooks();
        NewBookHistory = new BookHistory();
        Borrower = user;
        Lending = true;
    }

    private void ReturnABook(User user)
    {
        bookHistories = _userService.GetLendHistoryForBorrower(user.UserId);
        Borrower = user;
        Returning = true;
    }

    private void SearchBorrowers()
    {
        Users = _userService.SearchForBorrowers(searchCriteria);
    }

    private void ClearBorrowers()
    {
        searchCriteria = string.Empty;
        GetUsers();
    }

    private void SearchBook()
    {
        Books = _bookService.SearchBooks(searchCriteria);
    }

    private void ClearBook()
    {
        searchCriteria = string.Empty;
        GetBooks();
    }

    private void OpenDialogLend(Book book)
    {
        DialogIsOpenBook = true;
        wholebook = book;
        message = "Do you want to lend \"" + wholebook.Title + "\" to " + Borrower.Name + "?";
    }

    private async Task OnDialogCloseBook(bool isOk)
    {
        if (isOk)
        {
            _userService.CreateLend(wholebook, Borrower);
            Lending = false;
        }
        DialogIsOpenBook = false;
    }

    private void OpenDialogReturn(BookHistory History)
    {
        DialogIsOpenHist = true;
        NewBookHistory = History;
        message = "Has " + Borrower.Name + " returned \"" + NewBookHistory.Book.Title + "\"?";
    }

    private async Task OnDialogCloseHist(bool isOk)
    {
        if (isOk)
        {
            _userService.ReturnABook(NewBookHistory);
            Returning = false;
        }
        DialogIsOpenHist = false;
    }

    protected void OnKeyDown(KeyboardEventArgs e)
    {
        if (e.Key == "Enter" || e.Key == "NumpadEnter")
        {
            SearchBook();
        }
    }

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
