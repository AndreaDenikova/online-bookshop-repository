namespace OnlineBookshop.Data.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OnlineBookshop.Data.Common.Models;

public class Book : BaseDeletableModel<string>
{
    public Book()
    {
        this.Id = Guid.NewGuid().ToString();
    }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Cover { get; set; }

    public string Publisher { get; set; }

    public string Description { get; set; }

    public virtual Language Language { get; set; }

    [Required]
    public string LanguageId { get; set; }

    [Required]
    public decimal Price { get; set; }

    public decimal Rating { get; set; }

    public int Pages { get; set; }

    public int Year { get; set; }

    public string BookFile { get; set; }

    public virtual ICollection<AuthorBook> Authors { get; set; }

    public virtual ICollection<GenreBook> Genres { get; set; }
}
