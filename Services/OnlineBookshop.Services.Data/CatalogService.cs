namespace OnlineBookshop.Services.Data;

using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineBookshop.Data.Common.Repositories;
using OnlineBookshop.Data.Models;

public class CatalogService : ICatalogService
{
    private readonly IDeletableEntityRepository<Book> bookRepository;

    public CatalogService(IDeletableEntityRepository<Book> bookRepository)
    {
        this.bookRepository = bookRepository;
    }

    public IEnumerable<Book> GetBooks() =>
        this.bookRepository
            .All()
            .Include(b => b.Authors)
            .ThenInclude(a => a.Author)
            .Include(b => b.Genres)
            .ThenInclude(a => a.Genre)
            .ToList();
}
