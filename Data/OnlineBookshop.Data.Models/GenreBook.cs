namespace OnlineBookshop.Data.Models;

using OnlineBookshop.Data.Common.Models;

public class GenreBook : BaseDeletableModel<string>
{
    public virtual Book Book { get; set; }

    public string BookId { get; set; }

    public virtual Genre Genre { get; set; }

    public string GenreId { get; set; }
}
