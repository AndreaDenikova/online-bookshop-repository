namespace OnlineBookshop.Services.Data;

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using OnlineBookshop.Data.Common.Repositories;
using OnlineBookshop.Data.Models;
using OnlineBookshop.Web.ViewModels.InputModels;

public class BookService : IBookService
{
    private readonly IDeletableEntityRepository<Book> bookRepository;
    private readonly IDeletableEntityRepository<GenreBook> genreBookRepository;
    private readonly IDeletableEntityRepository<AuthorBook> authorBookRepository;
    private readonly IHostingEnvironment environment;

    public BookService(IDeletableEntityRepository<Book> bookRepository,
        IDeletableEntityRepository<GenreBook> genreBookRepository,
        IDeletableEntityRepository<AuthorBook> authorBookRepository,
        IHostingEnvironment environment)
    {
        this.bookRepository = bookRepository;
        this.genreBookRepository = genreBookRepository;
        this.authorBookRepository = authorBookRepository;
        this.environment = environment;
    }

    public async Task PostNewBookAsync(NewBookInputModel input)
    {
        string path = Path.Combine(this.environment.WebRootPath, "Uploads");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string fileName = DateTime.Now.Ticks + "_" + Path.GetFileName(input.Cover.FileName);
        using (var stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
        {
            input.Cover.CopyTo(stream);
        }

        var book = new Book
        {
            Title = input.Title,
            Description = input.Description,
            LanguageId = input.LanguageId,
            Pages = input.Pages,
            Price = input.Price,
            Year = input.Year,
            Publisher = input.Publisher,
            Cover = $"/Uploads/{fileName}",
        };

        await this.bookRepository.AddAsync(book);
        await this.bookRepository.SaveChangesAsync();

        foreach (var genreId in input.GenreIds)
        {
            var genreBook = new GenreBook
            {
                BookId = book.Id,
                GenreId = genreId,
            };
            await this.genreBookRepository.AddAsync(genreBook);
            await this.bookRepository.SaveChangesAsync();
        }

        var authorBook = new AuthorBook
        {
            AuthorId = input.AuthorId,
            BookId = book.Id,
        };

        await this.authorBookRepository.AddAsync(authorBook);

        await this.bookRepository.SaveChangesAsync();
    }
}
