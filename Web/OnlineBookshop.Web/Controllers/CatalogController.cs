namespace OnlineBookshop.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using OnlineBookshop.Data;
using OnlineBookshop.Services.Data;
using OnlineBookshop.Web.ViewModels.InputModels;
using OnlineBookshop.Web.ViewModels.ViewModels;
using System.Linq;

public class CatalogController : BaseController
{
    private readonly ApplicationDbContext dbContext;
    private readonly ICatalogService catalogService;

    public CatalogController(ApplicationDbContext dbContext, ICatalogService catalogService)
    {
        this.dbContext = dbContext;
        this.catalogService = catalogService;
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
}
