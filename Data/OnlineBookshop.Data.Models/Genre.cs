namespace OnlineBookshop.Data.Models;

using System;
using OnlineBookshop.Data.Common.Models;

public class Genre : BaseDeletableModel<string>
{
    public Genre()
    {
        this.Id = Guid.NewGuid().ToString();
    }

    public string Name { get; set; }

    public string Picture { get; set; }
}
