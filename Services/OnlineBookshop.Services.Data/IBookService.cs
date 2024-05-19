namespace OnlineBookshop.Services.Data;

using System.Threading.Tasks;
using OnlineBookshop.Data.Models;
using OnlineBookshop.Web.ViewModels.InputModels;

public interface IBookService
{
    Task PostNewBookAsync(NewBookInputModel input);

    Task DeleteBookAsync(string bookId);

    Task AddBookToFavoritesAsync(string userId, string bookId);

    Task RemoveBookFromFavoritesAsync(string userId, string bookId);

    Book GetBook(string bookId);
}
