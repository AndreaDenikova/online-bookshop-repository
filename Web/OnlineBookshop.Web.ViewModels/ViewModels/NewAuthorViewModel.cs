namespace OnlineBookshop.Web.ViewModels.ViewModels;

using System;
using OnlineBookshop.Web.ViewModels.InputModels;

public class NewAuthorViewModel
{
    public string Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Description { get; set; }

    public DateTime BirthDate { get; set; }

    public NewAuthorInputModel InputModel { get; set; }
}
