namespace OnlineBookshop.Services.Data;

using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineBookshop.Data.Models;
using OnlineBookshop.Web.ViewModels.InputModels;

public interface IBookService
{
    Task PostNewBookAsync(NewBookInputModel input);

    Task PostEditedBookAsync(NewBookInputModel input);

    Task DeleteBookAsync(string bookId);

    Task AddBookToFavoritesAsync(string userId, string bookId);

    Task AddBookToCartAsync(string userId, string bookId);

    Task RemoveBookFromFavoritesAsync(string userId, string bookId);

    Task RemoveBookFromCartAsync(string userId, string bookId);

    Task BuyBooksInCartAsync(string userId);

    Task ReportBookAsync(string userId, string bookId);

    Task RemoveBookFromReportedAsync(string bookId);

    Task<string> DeleteCommentFromRateAsync(string rateId);

    Task RateBookAsync(RateBookInputModel input, string userId);

    Book GetBook(string bookId);

    List<UserBookRate> GetBookReviews(string bookId);

    int GetCurrentPage(string bookId, string userId);

    double GetBookRatings(string bookId);

    bool IsBookOwned(string bookId, string userId);
}
