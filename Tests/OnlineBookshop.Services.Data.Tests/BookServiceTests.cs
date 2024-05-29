
namespace OnlineBookshop.Services.Data.Tests;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineBookshop.Data;
using OnlineBookshop.Data.Models;
using OnlineBookshop.Data.Repositories;
using Xunit;

public class BookServiceTests
{
    private IQueryable<Book> BookData() => new List<Book>
        {
            new()
            {
                Id = "1",
                Title = "Pretty Little Liars #1",
                Cover = "/Uploads/638514724063895111_prettylittleliars1.jpg",
                Publisher = "HarperTeen",
                Description = "Test description",
                LanguageId = "2",
                Price = 18,
                Rating = 4.50M,
                Pages = 125,
                Year = 2009,
                IsDeleted = false,
                BookFile = "/books/638514724063903533_Lorem_ipsum.pdf",
            },
            new()
            {
                Id = "2",
                Title = "Pretty Little Liars #2",
                Cover = "/Uploads/638514724063895111_prettylittleliars1.jpg",
                Publisher = "HarperTeen",
                Description = "Test description 2",
                LanguageId = "2",
                Price = 18,
                Rating = 4.50M,
                Pages = 125,
                Year = 2009,
                IsDeleted = true,
                BookFile = "/books/638514724063903533_Lorem_ipsum.pdf",
            },
            new()
            {
                Id = "3",
                Title = "Pretty Little Liars #3",
                Cover = "/Uploads/638514724063895111_prettylittleliars1.jpg",
                Publisher = "HarperTeen",
                Description = "Test description 3",
                LanguageId = "2",
                Price = 18,
                Rating = 4.50M,
                Pages = 125,
                Year = 2009,
                IsDeleted = false,
                BookFile = "/books/638514724063903533_Lorem_ipsum.pdf",
            },
        }.AsQueryable();

    //[Fact]
    //public async Task GetBookTest()
    //{
    //    var options = new DbContextOptionsBuilder<ApplicationDbContext>()
    //        .UseInMemoryDatabase(Guid.NewGuid().ToString());

    //    var bookRepository = new EfDeletableEntityRepository<Book>(new ApplicationDbContext(options.Options));

    //    foreach (var item in this.BookData())
    //    {
    //        await repository.AddAsync(item);
    //        await repository.SaveChangesAsync();
    //    }

    //    var service = new BookService(repository);

    //    service.GetBook("1");

    //    var favoritesCount = repository.All().ToList().Count;

    //    Assert.Equal(4, favoritesCount);
    //}
}
