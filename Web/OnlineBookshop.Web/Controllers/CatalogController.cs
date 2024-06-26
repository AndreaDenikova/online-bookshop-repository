﻿namespace OnlineBookshop.Web.Controllers;

using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineBookshop.Data;
using OnlineBookshop.Data.Models;
using OnlineBookshop.Services.Data;
using OnlineBookshop.Web.ViewModels.InputModels;
using OnlineBookshop.Web.ViewModels.ViewModels;

[Authorize]
public class CatalogController : BaseController
{
    private readonly ApplicationDbContext dbContext;
    private readonly ICatalogService catalogService;
    private readonly IBookService bookService;
    private readonly UserManager<ApplicationUser> userManager;

    public CatalogController(
        ApplicationDbContext dbContext,
        ICatalogService catalogService,
        IBookService bookService,
        UserManager<ApplicationUser> userManager)
    {
        this.dbContext = dbContext;
        this.catalogService = catalogService;
        this.bookService = bookService;
        this.userManager = userManager;
    }

    public IActionResult Catalog(CatalogFilterInputModel input)
    {
        var userId = this.userManager.GetUserId(this.User);
        var books = this.catalogService.GetBooks(input, userId).Select(b => new BookViewModel
        {
            Id = b.Id,
            Title = b.Title,
            CoverUrl = b.Cover,
            Price = b.Price,
            Authors = b.Authors?.Select(a => a.Author).ToList(),
            Rating = this.bookService.GetBookRatings(b.Id),
        }).OrderBy(b => b.Title).ToList();

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
        }).OrderBy(b => b.Title).ToList();

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

    public IActionResult GetCartCatalog(CatalogFilterInputModel input)
    {
        var userId = this.userManager.GetUserId(this.User);

        var books = this.catalogService.GetCartBooks(input, userId).Select(b => new BookViewModel
        {
            Id = b.Id,
            Title = b.Title,
            CoverUrl = b.Cover,
            Price = b.Price,
            Authors = b.Authors?.Select(a => a.Author).ToList(),
        }).OrderBy(b => b.Title).ToList();

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

    public IActionResult GetBookshelfCatalog(CatalogFilterInputModel input)
    {
        var userId = this.userManager.GetUserId(this.User);

        var books = this.catalogService.GetBookshelfBooks(input, userId).Select(b => new BookViewModel
        {
            Id = b.Id,
            Title = b.Title,
            CoverUrl = b.Cover,
            Price = b.Price,
            Authors = b.Authors?.Select(a => a.Author).ToList(),
            CurrentPage = this.bookService.GetCurrentPage(b.Id, userId),
        }).OrderBy(b => b.Title).ToList();

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

    public IActionResult ViewReportedBooks(CatalogFilterInputModel input)
    {
        var userId = this.userManager.GetUserId(this.User);

        var books = this.catalogService.GetReportedBooks(input, userId).Select(b => new BookViewModel
        {
            Id = b.Id,
            Title = b.Title,
            CoverUrl = b.Cover,
            Price = b.Price,
            Authors = b.Authors?.Select(a => a.Author).ToList(),
        }).OrderBy(b => b.Title).ToList();

        var genres = this.dbContext.Genre.ToList();

        input ??= new CatalogFilterInputModel();

        var model = new BookForCategoriesViewModel
        {
            Books = books,
            Genres = genres,
            InputModel = input,
        };

        return this.View(model);
    }
}
