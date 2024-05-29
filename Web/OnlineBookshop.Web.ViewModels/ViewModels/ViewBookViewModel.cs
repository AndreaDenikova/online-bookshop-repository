namespace OnlineBookshop.Web.ViewModels.ViewModels;

using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using OnlineBookshop.Data.Models;

public class ViewBookViewModel
{
    public string Id { get; set; }

    public IFormFile Cover { get; set; }

    public string CoverUrl { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Publisher { get; set; }

    public Language Language { get; set; }

    public int Year { get; set; }

    public decimal Price { get; set; }

    public int Pages { get; set; }

    public List<Author> Authors { get; set; }

    public List<Genre> Genres { get; set; }

    public List<UserBookRate> Rate { get; set; }
}
