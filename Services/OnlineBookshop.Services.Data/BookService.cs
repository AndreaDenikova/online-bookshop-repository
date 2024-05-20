namespace OnlineBookshop.Services.Data;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OnlineBookshop.Data.Common.Repositories;
using OnlineBookshop.Data.Models;
using OnlineBookshop.Web.ViewModels.InputModels;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class BookService : IBookService
{
    private readonly IDeletableEntityRepository<Book> bookRepository;
    private readonly IDeletableEntityRepository<GenreBook> genreBookRepository;
    private readonly IDeletableEntityRepository<AuthorBook> authorBookRepository;
    private readonly IDeletableEntityRepository<FavoriteBook> favoriteBookRepository;
    private readonly IDeletableEntityRepository<UserBookCart> userBookCartRepository;
    private readonly IHostingEnvironment environment;

    public BookService(
        IDeletableEntityRepository<Book> bookRepository,
        IDeletableEntityRepository<GenreBook> genreBookRepository,
        IDeletableEntityRepository<AuthorBook> authorBookRepository,
        IDeletableEntityRepository<FavoriteBook> favoriteBookRepository,
        IDeletableEntityRepository<UserBookCart> userBookCartRepository,
        IHostingEnvironment environment)
    {
        this.bookRepository = bookRepository;
        this.genreBookRepository = genreBookRepository;
        this.authorBookRepository = authorBookRepository;
        this.favoriteBookRepository = favoriteBookRepository;
        this.userBookCartRepository = userBookCartRepository;
        this.environment = environment;
    }

    public async Task AddBookToCartAsync(string userId, string bookId)
    {
        var book = this.userBookCartRepository.All().FirstOrDefault(f => f.UserId == userId && f.BookId == bookId);

        if (book == null)
        {
            var userBookCart = new UserBookCart
            {
                UserId = userId,
                BookId = bookId,
            };

            await this.userBookCartRepository.AddAsync(userBookCart);
            await this.userBookCartRepository.SaveChangesAsync();
        }
    }

    public async Task AddBookToFavoritesAsync(string userId, string bookId)
    {
        var favorite = this.favoriteBookRepository.All().FirstOrDefault(f => f.UserId == userId && f.BookId == bookId);
        if (favorite == null)
        {
            var favoriteBook = new FavoriteBook
            {
                UserId = userId,
                BookId = bookId,
            };

            await this.favoriteBookRepository.AddAsync(favoriteBook);
            await this.favoriteBookRepository.SaveChangesAsync();
        }
    }

    public async Task DeleteBookAsync(string bookId)
    {
        var book = this.bookRepository.All().Where(b => b.Id == bookId).FirstOrDefault();
        if (book != null)
        {
            book.IsDeleted = true;
            this.bookRepository.Update(book);
            await this.bookRepository.SaveChangesAsync();
        }
    }

    public Book GetBook(string bookId) => this.bookRepository
            .All()
            .Include(b => b.Authors)
            .ThenInclude(a => a.Author)
            .Include(b => b.Genres)
            .ThenInclude(a => a.Genre)
            .Single(b => b.Id == bookId);

    public async Task PostEditedBookAsync(NewBookInputModel input)
    {
        var book = this.bookRepository.All().Single(b => b.Id == input.Id);

        if (book != null)
        {
            book = new Book
            {
                Description = input.Description,
                Pages = input.Pages,
                Price = input.Price,
                Year = input.Year,
                LanguageId = input.LanguageId,
                BookFile = input.BookFileName,
                Cover = input.CoverName,
                Publisher = input.Publisher,
                Title = input.Title,
                // TODO: add ModifiedOn = DateTime.Now
                // TODO: add rating
            };

            var bookAuthors = this.authorBookRepository.All().Where(a => a.BookId == input.Id);
            await bookAuthors.ForEachAsync(b => this.authorBookRepository.HardDelete(b));
            var bookGenres = this.genreBookRepository.All().Where(a => a.BookId == input.Id);
            await bookGenres.ForEachAsync(b => this.genreBookRepository.HardDelete(b));

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

            this.bookRepository.Update(book);
            await this.bookRepository.SaveChangesAsync();
        }
    }

    public async Task PostNewBookAsync(NewBookInputModel input)
    {
        // TODO: add try catch
        string coverFileName = this.GetFileNameAndSave("Uploads", input.Cover);
        string bookFileName = this.GetFileNameAndSave("books", input.BookFile);

        var book = new Book
        {
            Title = input.Title,
            Description = input.Description,
            LanguageId = input.LanguageId,
            Pages = input.Pages,
            Price = input.Price,
            Year = input.Year,
            Publisher = input.Publisher,
            Cover = $"/Uploads/{coverFileName}",
            BookFile = $"/books/{bookFileName}",
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

    public async Task RemoveBookFromCartAsync(string userId, string bookId)
    {
        var cartBook = this.userBookCartRepository.All().Single(f => f.UserId == userId && f.BookId == bookId);
        this.userBookCartRepository.HardDelete(cartBook);

        await this.userBookCartRepository.SaveChangesAsync();
    }

    public async Task RemoveBookFromFavoritesAsync(string userId, string bookId)
    {
        var favoriteBook = this.favoriteBookRepository.All().Single(f => f.UserId == userId && f.BookId == bookId);
        this.favoriteBookRepository.HardDelete(favoriteBook);

        await this.favoriteBookRepository.SaveChangesAsync();
    }

    private string GetFileNameAndSave(string folder, IFormFile file)
    {
        string path = Path.Combine(this.environment.WebRootPath, folder);

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string uniqueFileName = DateTime.Now.Ticks + "_" + Path.GetFileName(file.FileName);
        using var stream = new FileStream(Path.Combine(path, uniqueFileName), FileMode.Create);
        file.CopyTo(stream);

        return uniqueFileName;
    }
}
