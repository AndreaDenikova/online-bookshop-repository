namespace OnlineBookshop.Services.Data;

using System.Threading.Tasks;
using OnlineBookshop.Web.ViewModels.InputModels;

public interface IAuthorService
{
    Task PostNewAuthorAsync(NewAuthorInputModel input);
}
