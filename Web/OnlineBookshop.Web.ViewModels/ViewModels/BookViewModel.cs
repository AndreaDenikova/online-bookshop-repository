namespace OnlineBookshop.Web.ViewModels.ViewModels;

using Microsoft.AspNetCore.Http;
using OnlineBookshop.Data.Models;
using System.Collections.Generic;

public class BookViewModel
{
    public string Id { get; set; }

    public IFormFile Cover { get; set; }

    public string CoverUrl { get; set; }

    public string Title { get; set; }

    public decimal Price { get; set; }

    public List<Author> Authors { get; set; }

    public double Rating { get; set; }

    public int CurrentPage { get; set; }
}
