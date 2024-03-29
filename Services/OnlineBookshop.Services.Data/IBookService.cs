﻿namespace OnlineBookshop.Services.Data;

using System.Threading.Tasks;
using OnlineBookshop.Web.ViewModels.InputModels;

public interface IBookService
{
    Task PostNewBookAsync(NewBookInputModel input);
}
