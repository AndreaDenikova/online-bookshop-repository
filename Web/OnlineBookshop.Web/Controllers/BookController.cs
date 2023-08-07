namespace OnlineBookshop.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using OnlineBookshop.Data;
using OnlineBookshop.Services.Data;
using OnlineBookshop.Web.ViewModels.InputModels;
using OnlineBookshop.Web.ViewModels.ViewModels;
using System.Linq;
using System.Threading.Tasks;

public class BookController : BaseController
{
    private readonly ApplicationDbContext dbContext;
    private readonly IBookService bookService;

    public BookController(ApplicationDbContext dbContext, IBookService bookService)
    {
        this.dbContext = dbContext;
        this.bookService = bookService;
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
}
