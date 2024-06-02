namespace OnlineBookshop.Web.Controllers;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineBookshop.Data;
using OnlineBookshop.Data.Models;
using OnlineBookshop.Services.Data;
using OnlineBookshop.Web.ViewModels.InputModels;
using OnlineBookshop.Web.ViewModels.ViewModels;

[Authorize]
public class AuthorController : BaseController
{
    private readonly ApplicationDbContext dbContext;
    private readonly ICatalogService catalogService;
    private readonly IAuthorService authorService;
    private readonly UserManager<ApplicationUser> userManager;

    public AuthorController(
        ApplicationDbContext dbContext,
        IAuthorService authorService,
        ICatalogService catalogService,
        UserManager<ApplicationUser> userManager)
    {
        this.dbContext = dbContext;
        this.authorService = authorService;
        this.catalogService = catalogService;
        this.userManager = userManager;
    }

    public IActionResult NewAuthor()
    {
        var model = new NewAuthorViewModel();

        return this.View(model);
    }

    [HttpPost]
    public async Task<IActionResult> NewAuthor(NewAuthorInputModel input)
    {
        await this.authorService.PostNewAuthorAsync(input);

        // TODO: is this page okay to redirect?
        return this.Redirect("/Catalog/Catalog");
    }
}
