namespace OnlineBookshop.Web.ViewModels.ViewModels;

using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using OnlineBookshop.Data.Models;
using OnlineBookshop.Services.Mapping;

public class BookViewModel
{
    public string Id { get; set; }

    public IFormFile Cover { get; set; }

    public string CoverUrl { get; set; }

    public string Title { get; set; }

    public decimal Price { get; set; }

    public List<Author> Authors { get; set; }
}
