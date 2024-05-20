namespace OnlineBookshop.Services.Data;

using OnlineBookshop.Data.Models;
using OnlineBookshop.Web.ViewModels.InputModels;
using System.Threading.Tasks;

public interface IBookService
{
    Task PostNewBookAsync(NewBookInputModel input);

    Task PostEditedBookAsync(NewBookInputModel input);

    Task DeleteBookAsync(string bookId);

    Task AddBookToFavoritesAsync(string userId, string bookId);

    Task AddBookToCartAsync(string userId, string bookId);

    Task RemoveBookFromFavoritesAsync(string userId, string bookId);

    Task RemoveBookFromCartAsync(string userId, string bookId);

    Book GetBook(string bookId);
}
