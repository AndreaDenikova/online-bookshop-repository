namespace OnlineBookshop.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineBookshop.Data;
using OnlineBookshop.Data.Models;
using OnlineBookshop.Services.Data;
using OnlineBookshop.Web.ViewModels.InputModels;
using OnlineBookshop.Web.ViewModels.ViewModels;
using System.Linq;
using System.Threading.Tasks;

[Authorize]
public class BookController : BaseController
{
    private readonly ApplicationDbContext dbContext;
    private readonly IBookService bookService;
    private readonly UserManager<ApplicationUser> userManager;

    public BookController(
        ApplicationDbContext dbContext,
        IBookService bookService,
        UserManager<ApplicationUser> userManager)
    {
        this.dbContext = dbContext;
        this.bookService = bookService;
        this.userManager = userManager;
    }

    public IActionResult NewBook()
    {
        var genres = this.dbContext.Genre.ToList();
        var authors = this.dbContext.Author.ToList();
        var languages = this.dbContext.Language.ToList();

        var model = new NewBookViewModel
        {
            Genres = genres,
            Authors = authors,
            Languages = languages,
        };

        return this.View(model);
    }

    [HttpPost]
    public async Task<IActionResult> NewBook(NewBookInputModel input)
    {
        await this.bookService.PostNewBookAsync(input);

        return this.Redirect("/Catalog/Catalog");
    }

    public IActionResult EditBook(string bookId)
    {
        var genres = this.dbContext.Genre.ToList();
        var authors = this.dbContext.Author.ToList();
        var languages = this.dbContext.Language.ToList();

        var book = this.bookService.GetBook(bookId);
        var fullBookFile = book.BookFile.Split('_').ToList();
        fullBookFile.RemoveAt(0);

        var inputModel = new NewBookInputModel
        {
            Id = bookId,
            Title = book.Title,
            AuthorId = book.Authors.Select(a => a.Id).Single(), // TODO: fix it to be possible to add more than 1 author
            Publisher = book.Publisher,
            GenreIds = book.Genres.Select(g => g.GenreId).ToList(),
            Pages = book.Pages,
            Year = book.Year,
            Description = book.Description,
            LanguageId = book.LanguageId,
            Price = book.Price,
            BookFileName = string.Join("_", fullBookFile),
            CoverName = book.Cover,
        };

        var model = new NewBookViewModel
        {
            Genres = genres,
            Authors = authors,
            Languages = languages,
            InputModel = inputModel,
        };

        this.ViewBag.BookId = book.Id;

        return this.View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditBook(NewBookInputModel input)
    {
        await this.bookService.PostEditedBookAsync(input);

        return this.Redirect("/Catalog/Catalog");
    }

    public async Task<IActionResult> DeleteBook(string id)
    {
        await this.bookService.DeleteBookAsync(id);

        return this.Redirect("/Catalog/Catalog");
    }

    public async Task<IActionResult> AddBookToFavorites(string bookId)
    {
        var userId = this.userManager.GetUserId(this.User);
        await this.bookService.AddBookToFavoritesAsync(userId, bookId);

        return this.Redirect("/Catalog/Catalog");
    }

    public async Task<IActionResult> RemoveBookFromFavorites(string bookId)
    {
        var userId = this.userManager.GetUserId(this.User);
        await this.bookService.RemoveBookFromFavoritesAsync(userId, bookId);

        return this.Redirect("/Catalog/GetFavoriteBooksCatalog");
    }

    public async Task<IActionResult> AddBookToCart(string bookId)
    {
        var userId = this.userManager.GetUserId(this.User);
        await this.bookService.AddBookToCartAsync(userId, bookId);

        return this.Redirect("/Catalog/Catalog");
    }

    public async Task<IActionResult> RemoveBookFromCart(string bookId)
    {
        var userId = this.userManager.GetUserId(this.User);
        await this.bookService.RemoveBookFromCartAsync(userId, bookId);

        return this.Redirect("/Catalog/GetCartCatalog");
    }

    public async Task<IActionResult> BuyBooksInCart()
    {
        var userId = this.userManager.GetUserId(this.User);
        await this.bookService.BuyBooksInCartAsync(userId);

        // TODO: change redirect page
        return this.Redirect("/Catalog/GetCartCatalog");
    }

    public IActionResult RateBook()
    {
        var model = new RateBookViewModel();

        return this.View(model);
    }

    [HttpPost]
    public async Task<IActionResult> RateBook(RateBookInputModel input)
    {
        var userId = this.userManager.GetUserId(this.User);
        await this.bookService.RateBookAsync(input, userId);

        return this.Redirect("/Catalog/GetBookshelfCatalog");
    }

    public async Task<IActionResult> DeleteComment(string rateId)
    {
        var bookId = await this.bookService.DeleteCommentFromRateAsync(rateId);

        // TODO: fix redirect
        return this.Redirect($"/Catalog/ViewBook?{bookId}");
    }

    public IActionResult ViewBook(string bookId)
    {
        var genres = this.dbContext.GenreBook.Where(b => b.BookId == bookId).Select(g => g.Genre).ToList();

        var authors = this.dbContext.AuthorBook.Where(b => b.BookId == bookId).Select(g => g.Author).ToList();

        var book = this.bookService.GetBook(bookId);
        var language = this.dbContext.Language.Single(l => l.Id == book.LanguageId);

        var fullBookFile = book.BookFile.Split('_').ToList();
        fullBookFile.RemoveAt(0);

        var model = new ViewBookViewModel
        {
            Id = bookId,
            Title = book.Title,
            Authors = authors, // TODO: fix it to be possible to add more than 1 author
            Publisher = book.Publisher,
            Genres = genres,
            Pages = book.Pages,
            Year = book.Year,
            Description = book.Description,
            Language = language, // TODO: fix it to be possible to add more than 1 language ??
            Price = book.Price,
            CoverUrl = book.Cover,
            Rate = this.bookService.GetBookReviews(bookId),
        };

        return this.View(model);
    }

    public async Task<IActionResult> ReportBook(string bookId)
    {
        var userId = this.userManager.GetUserId(this.User);
        await this.bookService.ReportBookAsync(userId, bookId);

        return this.Redirect("/Catalog/Catalog");
    }

    public async Task<IActionResult> RemoveBookFromReported(string bookId)
    {
        await this.bookService.RemoveBookFromReportedAsync(bookId);

        return this.Redirect("/Catalog/ViewReportedBooks");
    }
}
