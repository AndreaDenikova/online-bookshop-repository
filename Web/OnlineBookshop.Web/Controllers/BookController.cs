namespace OnlineBookshop.Web.Controllers;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineBookshop.Data;
using OnlineBookshop.Data.Models;
using OnlineBookshop.Services.Data;
using OnlineBookshop.Web.ViewModels.InputModels;
using OnlineBookshop.Web.ViewModels.ViewModels;

public class BookController : BaseController
{
    private readonly ApplicationDbContext dbContext;
    private readonly IBookService bookService;
    private readonly UserManager<ApplicationUser> userManager;

    public BookController(ApplicationDbContext dbContext, IBookService bookService,
        UserManager<ApplicationUser> userManager)
    {
        this.dbContext = dbContext;
        this.bookService = bookService;
        this.userManager = userManager;
    }

    [Authorize]
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

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> NewBook(NewBookInputModel input)
    {
        await this.bookService.PostNewBookAsync(input);

        return this.Redirect("/Catalog/Catalog");
    }

    [Authorize]
    public IActionResult EditBook()
    {
        //var genres = this.dbContext.Genre.ToList();
        //var authors = this.dbContext.Author.ToList();
        //var languages = this.dbContext.Language.ToList();

        //var model = new NewBookViewModel
        //{
        //    Genres = genres,
        //    Authors = authors,
        //    Languages = languages,
        //};

        return this.Redirect("/Catalog/Catalog");
    }

    [Authorize]
    public async Task<IActionResult> DeleteBook(string id)
    {
        await this.bookService.DeleteBookAsync(id);

        return this.Redirect("/Catalog/Catalog");
    }

    [Authorize]
    public async Task<IActionResult> AddBookToFavorites(string bookId)
    {
        var userId = this.userManager.GetUserId(this.User);
        await this.bookService.AddBookToFavoritesAsync(userId, bookId);

        return this.Redirect("/Catalog/Catalog");
    }
}
