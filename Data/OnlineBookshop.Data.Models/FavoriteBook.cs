namespace OnlineBookshop.Data.Models;

using System;
using OnlineBookshop.Data.Common.Models;

public class FavoriteBook : BaseDeletableModel<string>
{
    public FavoriteBook()
    {
        this.Id = Guid.NewGuid().ToString();
    }

    public virtual ApplicationUser User { get; set; }

    public string UserId { get; set; }

    public virtual Book Book { get; set; }

    public string BookId { get; set; }
}
