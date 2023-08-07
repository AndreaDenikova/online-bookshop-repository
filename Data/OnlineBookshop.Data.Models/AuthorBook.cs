namespace OnlineBookshop.Data.Models;

using OnlineBookshop.Data.Common.Models;

public class AuthorBook : BaseDeletableModel<string>
{
    public virtual Author Author { get; set; }

    public string AuthorId { get; set; }

    public virtual Book Book { get; set; }

    public string BookId { get; set; }
}
