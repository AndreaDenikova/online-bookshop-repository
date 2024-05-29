namespace OnlineBookshop.Services.Data;

using System.Threading.Tasks;
using OnlineBookshop.Data.Models;
using OnlineBookshop.Web.ViewModels.InputModels;

public interface IAuthorService
{
    Task PostNewAuthorAsync(NewAuthorInputModel input);

    Author GetAuthor(string authorId);
}
