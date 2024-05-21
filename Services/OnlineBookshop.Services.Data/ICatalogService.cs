namespace OnlineBookshop.Services.Data;

using System.Collections.Generic;
using OnlineBookshop.Data.Models;
using OnlineBookshop.Web.ViewModels.InputModels;
public interface ICatalogService
{
    IEnumerable<Book> GetBooks(CatalogFilterInputModel input);

    IEnumerable<Book> GetFavoriteBooks(CatalogFilterInputModel input, string userId);

    IEnumerable<Book> GetCartBooks(CatalogFilterInputModel input, string userId);

    IEnumerable<Book> GetBookshelfBooks(CatalogFilterInputModel input, string userId);
}
