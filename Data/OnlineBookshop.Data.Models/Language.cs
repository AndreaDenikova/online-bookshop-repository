namespace OnlineBookshop.Data.Models;

using System;

using OnlineBookshop.Data.Common.Models;

public class Language : BaseDeletableModel<string>
{
    public Language()
    {
        this.Id = Guid.NewGuid().ToString();
    }

    public string Name { get; set; }
}
