﻿namespace OnlineBookshop.Web.ViewModels.InputModels;

using System;

public class NewAuthorInputModel
{
    public string Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Description { get; set; }

    public DateTime BirthDate { get; set; }
}
