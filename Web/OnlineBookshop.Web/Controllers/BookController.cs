﻿namespace OnlineBookshop.Web.Controllers;

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        return this.View(model);
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

    public async Task<IActionResult> RemoveBookFromFavorites(string bookId)
    {
        var userId = this.userManager.GetUserId(this.User);
        await this.bookService.RemoveBookFromFavoritesAsync(userId, bookId);

        return this.Redirect("/Catalog/GetFavoriteBooksCatalog");
    }
}
