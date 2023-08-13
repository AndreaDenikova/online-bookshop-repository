using OnlineBookshop.Data.Models;
using OnlineBookshop.Web.ViewModels.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookshop.Services.Data
{
    public interface ICatalogService
    {
        IEnumerable<Book> GetBooks(CatalogFilterInputModel input);
    }
}
