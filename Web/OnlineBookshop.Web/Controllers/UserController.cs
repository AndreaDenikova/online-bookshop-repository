namespace OnlineBookshop.Web.Controllers;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineBookshop.Data;
using OnlineBookshop.Data.Models;

public class UserController : BaseController
{
    private readonly ApplicationDbContext dbContext;
    private readonly UserManager<ApplicationUser> userManager;

    public UserController(
        ApplicationDbContext dbContext,
        UserManager<ApplicationUser> userManager)
    {
        this.dbContext = dbContext;
        this.userManager = userManager;
    }

    //[Authorize]
    //public async Task<IActionResult> UpdatePasword(string newEmail)
    //{
    //    var user = await this.userManager.GetUserAsync(this.User);
    //    await userManager.ChangeEmailAsync(user, newEmail);

    //    //var model = new NewBookViewModel
    //    //{
    //    //    Genres = genres,
    //    //    Authors = authors,
    //    //    Languages = languages,
    //    //};

    //    return this.View(model);
    //}

    //[Authorize]
    //public async Task<IActionResult> UpdateEmail(string newPassword)
    //{
    //    var user = await this.userManager.GetUserAsync(this.User);

    //    await userManager.ChangePasswordAsync(user, user.PasswordHash, newPassword);
    //    //var model = new NewBookViewModel
    //    //{
    //    //    Genres = genres,
    //    //    Authors = authors,
    //    //    Languages = languages,
    //    //};

    //    //return this.View(model);
    //}
}
