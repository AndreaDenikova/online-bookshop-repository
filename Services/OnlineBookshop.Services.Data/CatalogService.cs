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

    public CatalogService(
        IDeletableEntityRepository<Book> bookRepository,
        IDeletableEntityRepository<GenreBook> genreBookRepository)
    {
        this.bookRepository = bookRepository;
        this.genreBookRepository = genreBookRepository;
    }

    public IEnumerable<Book> GetBooks(CatalogFilterInputModel input)
    {
        var result = new List<Book>();

        if (input.GenreIds != null && input.GenreIds.Any())
        {
            var booksIdsByGenre = this.genreBookRepository.All().Where(b => input.GenreIds.Contains(b.GenreId));
            result.AddRange(this.bookRepository
                    .All()
                    .Include(b => b.Authors)
                    .ThenInclude(a => a.Author)
                    .Include(b => b.Genres)
                    .ThenInclude(a => a.Genre)
                    .Where(b => booksIdsByGenre
                    .Select(b => b.BookId)
                    .Contains(b.Id))
                .ToList());
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
        }

        var notDletedBooks = result.Where(b => !b.IsDeleted);

        if (notDletedBooks.Any())
        {
            return notDletedBooks.DistinctBy(b => b.Id);
        }

        return this.bookRepository
            .All()
            .Include(b => b.Authors)
            .ThenInclude(a => a.Author)
            .Include(b => b.Genres)
            .ThenInclude(a => a.Genre)
            .Where(b => !b.IsDeleted)
            .ToList();
    }
}
