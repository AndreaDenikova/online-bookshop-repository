namespace OnlineBookshop.Data.Models;

using System;
using OnlineBookshop.Data.Common.Models;

public class Author : BaseDeletableModel<string>
{
    public Author()
    {
        this.Id = Guid.NewGuid().ToString();
    }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime? BirthDate { get; set; }

    public string Description { get; set; }
}
