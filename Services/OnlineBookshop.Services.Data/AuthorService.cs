namespace OnlineBookshop.Services.Data;

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using OnlineBookshop.Data.Common.Repositories;
using OnlineBookshop.Data.Models;
using OnlineBookshop.Web.ViewModels.InputModels;

public class AuthorService : IAuthorService
{
    private readonly IDeletableEntityRepository<Author> authorRepository;
    private readonly IHostingEnvironment environment;

    public AuthorService(
        IDeletableEntityRepository<Author> authorRepository,
        IHostingEnvironment environment)
    {
        this.authorRepository = authorRepository;
        this.environment = environment;
    }

    public async Task PostNewAuthorAsync(NewAuthorInputModel input)
    {
        var author = new Author()
        {
            FirstName = input.FirstName,
            LastName = input.LastName,
            BirthDate = input.BirthDate,
            Description = input.Description,
        };

        await this.authorRepository.AddAsync(author);
        await this.authorRepository.SaveChangesAsync();
    }
}
