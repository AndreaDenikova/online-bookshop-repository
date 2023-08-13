namespace OnlineBookshop.Web.ViewModels.ViewModels;

using System.Collections.Generic;
using OnlineBookshop.Data.Models;
using OnlineBookshop.Web.ViewModels.InputModels;

public class BookForCategoriesViewModel
{
    public List<BookViewModel> Books { get; set; }

    public List<Genre> Genres { get; set; }

    public CatalogFilterInputModel InputModel { get; set; }
}
