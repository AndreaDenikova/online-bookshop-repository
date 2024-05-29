using Microsoft.EntityFrameworkCore;
using OnlineBookshop.Data;
using OnlineBookshop.Data.Models;
using OnlineBookshop.Data.Repositories;
using OnlineBookshop.Web.ViewModels.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OnlineBookshop.Services.Data.Tests
{
    public class AuthorServiceTests
    {
        [Fact]
        public async Task NewAuthorTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var authorRepository = new EfDeletableEntityRepository<Author>(new ApplicationDbContext(options.Options));

            foreach (var item in this.AuthorData())
            {
                await authorRepository.AddAsync(item);
                await authorRepository.SaveChangesAsync();
            }

            var service = new AuthorService(authorRepository);

            var inputModel = new NewAuthorInputModel
            {
                Id = "4",
                BirthDate = new DateTime(2005, 05, 29),
                FirstName = "Todor",
                LastName = "Dimitrov",
                Description = "Description",
            };

            await service.PostNewAuthorAsync(inputModel);

            var authors = authorRepository.All().ToList();

            Assert.Equal(3, authors.Count);
        }

        private IQueryable<Author> AuthorData()
        {
            return new List<Author>
        {
            new()
            {
                Id = "1",
                FirstName = "Andrea",
                LastName = "Denikova",
                BirthDate = new DateTime(2005, 05, 29),
                Description = "Description",
                IsDeleted = false,
            },
            new()
            {
                Id = "2",
                FirstName = "Mona",
                LastName = "Lisa",
                BirthDate = new DateTime(2002, 05, 28),
                Description = "Description 2",
                IsDeleted = false,
            },
            new()
            {
                Id = "3",
                FirstName = "Petar",
                LastName = "Petrov",
                BirthDate = new DateTime(1999, 05, 27),
                Description = "Description 3",
                IsDeleted = true,
            },
        }.AsQueryable();
        }
    }
}
