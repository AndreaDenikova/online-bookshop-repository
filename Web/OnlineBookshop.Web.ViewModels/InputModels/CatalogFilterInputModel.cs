namespace OnlineBookshop.Web.ViewModels.InputModels;

using System.Collections.Generic;

public class CatalogFilterInputModel
{
    public string AuthorBookTitle { get; set; }

    public List<string> GenreIds { get; set; }
}
