namespace OnlineBookshop.Web.Controllers;

using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineBookshop.Data;
using OnlineBookshop.Data.Models;
using OnlineBookshop.Services.Data;
using OnlineBookshop.Web.ViewModels.InputModels;
using OnlineBookshop.Web.ViewModels.ViewModels;

public class CatalogController : BaseController
{
    private readonly ApplicationDbContext dbContext;
    private readonly ICatalogService catalogService;
    private readonly UserManager<ApplicationUser> userManager;

    public CatalogController(ApplicationDbContext dbContext, ICatalogService catalogService,
        UserManager<ApplicationUser> userManager)
    {
        this.dbContext = dbContext;
        this.catalogService = catalogService;
        this.userManager = userManager;
    }

    public IActionResult Catalog(CatalogFilterInputModel input)
    {
        var books = this.catalogService.GetBooks(input).Select(b => new BookViewModel
        {
            Id = b.Id,
            Title = b.Title,
            CoverUrl = b.Cover,
            Price = b.Price,
            Authors = b.Authors?.Select(a => a.Author).ToList(),
        }).ToList();

        var genres = this.dbContext.Genre.ToList();

        if (input == null)
        {
            input = new CatalogFilterInputModel();
        }

        var model = new BookForCategoriesViewModel
        {
            Books = books,
            Genres = genres,
            InputModel = input,
        };

        return this.View(model);
    }

    public IActionResult GetFavoriteBooksCatalog(CatalogFilterInputModel input)
    {
        var userId = this.userManager.GetUserId(this.User);

        var books = this.catalogService.GetFavoriteBooks(input, userId).Select(b => new BookViewModel
        {
            Id = b.Id,
            Title = b.Title,
            CoverUrl = b.Cover,
            Price = b.Price,
            Authors = b.Authors?.Select(a => a.Author).ToList(),
        }).ToList();

        var genres = this.dbContext.Genre.ToList();

        if (input == null)
        {
            input = new CatalogFilterInputModel();
        }

        var model = new BookForCategoriesViewModel
        {
            Books = books,
            Genres = genres,
            InputModel = input,
        };

        return this.View(model);
    }
}
