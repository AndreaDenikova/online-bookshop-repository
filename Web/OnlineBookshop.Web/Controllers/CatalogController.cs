namespace OnlineBookshop.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using OnlineBookshop.Services.Data;

public class CatalogController : BaseController
{
    private readonly ICatalogService catalogService;

    public CatalogController(ICatalogService catalogService)
    {
        this.catalogService = catalogService;
    }

    public IActionResult Catalog()
    {
        var books = this.catalogService.GetBooks();

        return this.View(books);
    }
}
