namespace OnlineBookshop.Data.Models;

using OnlineBookshop.Data.Common.Models;

public class UserBookRate : BaseDeletableModel<string>
{
    public virtual ApplicationUser User { get; set; }

    public string UserId { get; set; }

    public virtual Book Book { get; set; }

    public string BookId { get; set; }

    public int Rating { get; set; }

    public string Comment { get; set; }
}
