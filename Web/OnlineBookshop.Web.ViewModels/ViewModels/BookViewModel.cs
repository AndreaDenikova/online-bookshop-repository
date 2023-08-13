using Microsoft.AspNetCore.Http;
using OnlineBookshop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookshop.Web.ViewModels.ViewModels
{
    public class BookViewModel
    {
        public IFormFile Cover { get; set; }

        public string CoverUrl { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public List<Author> Authors { get; set; }
    }
}
