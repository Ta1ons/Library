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
#line 1 "C:\Gitprojects\Library\library\LibraryServerApp\Pages\BookOptions.razor"
using LibraryService.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Gitprojects\Library\library\LibraryServerApp\Pages\BookOptions.razor"
using LibraryService.Data.Models;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/Books")]
    public partial class BookOptions : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 70 "C:\Gitprojects\Library\library\LibraryServerApp\Pages\BookOptions.razor"
       

    private bool AddingNewBook = false;
    private BookService _bookService;

    private List<Book> Books = new List<Book>();
    private Book BookToAdd;


    private string searchCriteria = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        _bookService = new BookService();
        GetBooks();
    }

    private void GetBooks()
    {
        Books = _bookService.GetAllBooks();
    }

    private void NotAdding()
    {
        AddingNewBook = false;
    }

    private void Adding()
    {
        BookToAdd = new Book();
        AddingNewBook = true;
    }

    private void EditBook(Book book)
    {
        if (book != null)
        {
            BookToAdd = book;
        }
        AddingNewBook = true;
    }


        private void SaveBook()
        {
            if (AddingNewBook == true)
            {
                _bookService.SaveBook(BookToAdd);
            }

            GetBooks();
            NotAdding();
        }

        private void DeleteBook(int bookID)
        {
            _bookService.DeleteBook(bookID);
            GetBooks();
        }

        private void Search()
        {
            Books = _bookService.SearchBooks(searchCriteria);
        }

        private void Clear()
        {
            searchCriteria = string.Empty;
            GetBooks();
        }
    

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
