namespace OnlineBookshop.Services.Data;

using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineBookshop.Data.Common.Repositories;
using OnlineBookshop.Data.Models;
using OnlineBookshop.Web.ViewModels.InputModels;

public class CatalogService : ICatalogService
{
    private readonly IDeletableEntityRepository<Book> bookRepository;
    private readonly IDeletableEntityRepository<GenreBook> genreBookRepository;
    private readonly IDeletableEntityRepository<FavoriteBook> favoriteBookRepository;
    private readonly IDeletableEntityRepository<UserBookCart> userBookCartRepository;
    private readonly IDeletableEntityRepository<UserBook> userBookRepository;
    private readonly IDeletableEntityRepository<UserBookRate> userBookRateRepository;

    public CatalogService(
        IDeletableEntityRepository<Book> bookRepository,
        IDeletableEntityRepository<GenreBook> genreBookRepository,
        IDeletableEntityRepository<FavoriteBook> favoriteBookRepository,
        IDeletableEntityRepository<UserBookCart> userBookCartRepository,
        IDeletableEntityRepository<UserBook> userBookRepository,
        IDeletableEntityRepository<UserBookRate> userBookRateRepository)
    {
        this.bookRepository = bookRepository;
        this.genreBookRepository = genreBookRepository;
        this.favoriteBookRepository = favoriteBookRepository;
        this.userBookCartRepository = userBookCartRepository;
        this.userBookRepository = userBookRepository;
        this.userBookRateRepository = userBookRateRepository;
    }

    public IEnumerable<Book> GetBooks(CatalogFilterInputModel input, string userId)
    {
        var result = new List<Book>();
        var searchIsMade = false;
        if (input.GenreIds != null && input.GenreIds.Any())
        {
            var booksIdsByGenre = this.genreBookRepository.All().Where(b => input.GenreIds.Contains(b.GenreId));
            result.AddRange(this.bookRepository
                    .All()
                    .Where(b => booksIdsByGenre
                    .Select(b => b.BookId)
                    .Contains(b.Id))
                    .Include(b => b.Authors)
                    .ThenInclude(a => a.Author)
                    .Include(b => b.Genres)
                    .ThenInclude(a => a.Genre)
                .ToList());

            searchIsMade = true;
        }

        if (input.AuthorBookTitle?.Length > 0)
        {
            var splittedAuthorBookTitle = input.AuthorBookTitle.Split(", ");

            foreach (var authorOrBookTitle in splittedAuthorBookTitle)
            {
                result.AddRange(this.bookRepository
                        .All()
                        .Include(b => b.Authors)
                        .ThenInclude(a => a.Author)
                        .Include(b => b.Genres)
                        .ThenInclude(a => a.Genre)
                        .Where(b => b.Authors
                        .Any(x => (x.Author.FirstName + " " + x.Author.LastName).Contains(authorOrBookTitle)) ||
                            b.Title.Contains(authorOrBookTitle))
                    .ToList());
            }

            searchIsMade = true;
        }

        var ownedBooks = this.userBookRepository.All().Where(b => b.UserId == userId).Select(b => b.BookId).ToList();

        if (searchIsMade)
        {
            return result
                .Where(b =>
                    !b.IsDeleted &&
                    !ownedBooks.Contains(b.Id))
                .DistinctBy(b => b.Id);
        }

        return this.bookRepository
            .All()
            .Include(b => b.Authors)
            .ThenInclude(a => a.Author)
            .Include(b => b.Genres)
            .ThenInclude(a => a.Genre)
            .Where(b => !b.IsDeleted && !ownedBooks.Contains(b.Id))
            .ToList();
    }

    public IEnumerable<Book> GetBookshelfBooks(CatalogFilterInputModel input, string userId)
    {
        var result = new List<Book>();
        var searchIsMade = false;
        var userBookshelfBooksIds = this.userBookRepository.All().Where(f => f.UserId == userId).Select(f => f.BookId);

        if (input.GenreIds != null && input.GenreIds.Any())
        {
            var booksIdsByGenre = this.genreBookRepository.All().Where(b => input.GenreIds.Contains(b.GenreId));
            result.AddRange(this.bookRepository
                    .All()
                    .Where(b => booksIdsByGenre
                    .Select(b => b.BookId)
                    .Contains(b.Id))
                    .Include(b => b.Authors)
                    .ThenInclude(a => a.Author)
                    .Include(b => b.Genres)
                    .ThenInclude(a => a.Genre)
                .ToList());

            searchIsMade = true;
        }

        if (input.AuthorBookTitle?.Length > 0)
        {
            var splittedAuthorBookTitle = input.AuthorBookTitle.Split(", ");

            foreach (var authorOrBookTitle in splittedAuthorBookTitle)
            {
                result.AddRange(this.bookRepository
                        .All()
                        .Include(b => b.Authors)
                        .ThenInclude(a => a.Author)
                        .Include(b => b.Genres)
                        .ThenInclude(a => a.Genre)
                        .Where(b => b.Authors
                        .Any(x => (x.Author.FirstName + " " + x.Author.LastName).Contains(authorOrBookTitle)) ||
                            b.Title.Contains(authorOrBookTitle))
                    .ToList());
            }

            searchIsMade = true;
        }

        if (searchIsMade)
        {
            return result
                .Where(b =>
                    !b.IsDeleted &&
                    userBookshelfBooksIds.Contains(b.Id))
                .DistinctBy(b => b.Id);
        }

        return this.bookRepository
            .All()
            .Include(b => b.Authors)
            .ThenInclude(a => a.Author)
            .Include(b => b.Genres)
            .ThenInclude(a => a.Genre)
            .Where(b => !b.IsDeleted && userBookshelfBooksIds.Contains(b.Id))
            .ToList();
    }

    public IEnumerable<Book> GetCartBooks(CatalogFilterInputModel input, string userId)
    {
        var result = new List<Book>();
        var searchIsMade = false;
        var userFavoriteBooksIds = this.userBookCartRepository.All().Where(f => f.UserId == userId).Select(f => f.BookId);

        if (input.GenreIds != null && input.GenreIds.Any())
        {
            var booksIdsByGenre = this.genreBookRepository.All().Where(b => input.GenreIds.Contains(b.GenreId));
            result.AddRange(this.bookRepository
                    .All()
                    .Where(b => booksIdsByGenre
                    .Select(b => b.BookId)
                    .Contains(b.Id))
                    .Include(b => b.Authors)
                    .ThenInclude(a => a.Author)
                    .Include(b => b.Genres)
                    .ThenInclude(a => a.Genre)
                .ToList());

            searchIsMade = true;
        }

        if (input.AuthorBookTitle?.Length > 0)
        {
            var splittedAuthorBookTitle = input.AuthorBookTitle.Split(", ");

            foreach (var authorOrBookTitle in splittedAuthorBookTitle)
            {
                result.AddRange(this.bookRepository
                        .All()
                        .Include(b => b.Authors)
                        .ThenInclude(a => a.Author)
                        .Include(b => b.Genres)
                        .ThenInclude(a => a.Genre)
                        .Where(b => b.Authors
                        .Any(x => (x.Author.FirstName + " " + x.Author.LastName).Contains(authorOrBookTitle)) ||
                            b.Title.Contains(authorOrBookTitle))
                    .ToList());
            }

            searchIsMade = true;
        }

        if (searchIsMade)
        {
            return result.Where(b => !b.IsDeleted && userFavoriteBooksIds.Contains(b.Id)).DistinctBy(b => b.Id);
        }

        return this.bookRepository
            .All()
            .Include(b => b.Authors)
            .ThenInclude(a => a.Author)
            .Include(b => b.Genres)
            .ThenInclude(a => a.Genre)
            .Where(b => !b.IsDeleted && userFavoriteBooksIds.Contains(b.Id))
            .ToList();
    }

    public IEnumerable<Book> GetFavoriteBooks(CatalogFilterInputModel input, string userId)
    {
        var result = new List<Book>();
        var searchIsMade = false;
        var userFavoriteBooksIds = this.favoriteBookRepository.All().Where(f => f.UserId == userId).Select(f => f.BookId);

        if (input.GenreIds != null && input.GenreIds.Any())
        {
            var booksIdsByGenre = this.genreBookRepository.All().Where(b => input.GenreIds.Contains(b.GenreId));
            result.AddRange(this.bookRepository
                    .All()
                    .Where(b => booksIdsByGenre
                    .Select(b => b.BookId)
                    .Contains(b.Id))
                    .Include(b => b.Authors)
                    .ThenInclude(a => a.Author)
                    .Include(b => b.Genres)
                    .ThenInclude(a => a.Genre)
                .ToList());

            searchIsMade = true;
        }

        if (input.AuthorBookTitle?.Length > 0)
        {
            var splittedAuthorBookTitle = input.AuthorBookTitle.Split(", ");

            foreach (var authorOrBookTitle in splittedAuthorBookTitle)
            {
                result.AddRange(this.bookRepository
                        .All()
                        .Include(b => b.Authors)
                        .ThenInclude(a => a.Author)
                        .Include(b => b.Genres)
                        .ThenInclude(a => a.Genre)
                        .Where(b => b.Authors
                        .Any(x => (x.Author.FirstName + " " + x.Author.LastName).Contains(authorOrBookTitle)) ||
                            b.Title.Contains(authorOrBookTitle))
                    .ToList());
            }

            searchIsMade = true;
        }

        var ownedBooks = this.userBookRepository.All().Where(b => b.UserId == userId).Select(b => b.BookId).ToList();

        if (searchIsMade)
        {
            return result
                .Where(b =>
                    !b.IsDeleted &&
                    userFavoriteBooksIds.Contains(b.Id) &&
                    !ownedBooks.Contains(b.Id))
                .DistinctBy(b => b.Id);
        }

        return this.bookRepository
            .All()
            .Include(b => b.Authors)
            .ThenInclude(a => a.Author)
            .Include(b => b.Genres)
            .ThenInclude(a => a.Genre)
            .Where(b => !b.IsDeleted && userFavoriteBooksIds.Contains(b.Id) && !ownedBooks.Contains(b.Id))
            .ToList();
    }
}
