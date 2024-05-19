namespace OnlineBookshop.Web.ViewModels.InputModels;

using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

public class NewBookInputModel
{
    public IFormFile Cover { get; set; }

    public string CoverName { get; set; }

    public IFormFile BookFile { get; set; }

    public string BookFileName { get; set; }

    public string Title { get; set; }

    public string AuthorId { get; set; }

    public string Publisher { get; set; }

    public List<string> GenreIds { get; set; }

    public int Pages { get; set; }

    public int Year { get; set; }

    public string Description { get; set; }

    public string LanguageId { get; set; }

    public decimal Price { get; set; }
}
