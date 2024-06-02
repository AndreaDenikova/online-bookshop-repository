
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBookshop.Data;
using OnlineBookshop.Data.Models;
using OnlineBookshop.Services.Data;
using OnlineBookshop.Web.ViewModels.ViewModels;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OnlineBookshop.Web.Controllers
{
    [Authorize]
    public class ReaderController : BaseController
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IBookService bookService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserReaderSettingsService userReaderSettingsService;

        public ReaderController(
        ApplicationDbContext dbContext,
        IBookService bookService,
        UserManager<ApplicationUser> userManager,
        IUserReaderSettingsService userReaderSettingsService)
        {
            this.dbContext = dbContext;
            this.bookService = bookService;
            this.userManager = userManager;
            this.userReaderSettingsService = userReaderSettingsService;
        }

        [Route("/Reader/Book/{bookId}/{page}")]
        public IActionResult Book(string bookId, int page)
        {
            var book = this.bookService.GetBook(bookId);

            if (!this.bookService.IsBookOwned(bookId, this.userManager.GetUserId(this.User)))
            {
                // TODO: add error page
                throw new Exception("Book not owned!");
            }

            Winnovative.PdfToText.PdfToTextConverter pdfToTextConverter = new Winnovative.PdfToText.PdfToTextConverter();
            var pathToBook = Environment.CurrentDirectory + "/wwwroot" + book.BookFile;
            var text = pdfToTextConverter.ConvertToText(pathToBook, page, page);
            var pageCount = pdfToTextConverter.GetPageCount(pathToBook);

            text = text.Replace("***********************************************************************************", "");
            text = text.Replace("You are currently running a DEMO version of the PDF to Text Converter for .NET.", "");
            text = text.Replace("You can obtain a license from http://www.winnovative-software.com.\r\n\r\n\r\n", "");
            text = text.Replace("Winnovative Demo - http://www.winnovative-software.com\r\n", "");
            text = text.Replace("\r\n\r\n", "\r\n");

            this.ViewBag.Text = text;
            this.ViewBag.Page = page;
            this.ViewBag.PageCount = pageCount;
            this.ViewBag.BookId = bookId;

            var userBook = this.dbContext
                .UserBook
                .FirstOrDefault(x => x.BookId == bookId && x.UserId == this.userManager.GetUserId(this.User));

            if (userBook != null)
            {
                userBook.CurrentPage = page;
                this.dbContext.SaveChanges();
            }

            var viewModel = this.userReaderSettingsService.GetReaderSettings(this.userManager.GetUserId(this.User)) ??
                new UserReaderSettingsViewModel();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SaveReaderSettings(UserReaderSettingsViewModel model, string bookId, int page)
        {
            await this.userReaderSettingsService.SaveReaderSettings(model, this.userManager.GetUserId(this.User));

            return this.Redirect($"/Reader/Book/{bookId}/{page}");
        }
    }
}
