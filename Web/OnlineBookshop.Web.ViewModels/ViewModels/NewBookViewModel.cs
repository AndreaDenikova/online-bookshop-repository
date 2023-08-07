namespace OnlineBookshop.Web.ViewModels.ViewModels;

using System.Collections.Generic;
using OnlineBookshop.Data.Models;
using OnlineBookshop.Web.ViewModels.InputModels;

public class NewBookViewModel
{
    public List<Genre> Genres { get; set; }

    public List<Language> Languages { get; set; }

    public List<Author> Authors { get; set; }

    public NewBookInputModel InputModel { get; set; }
}
