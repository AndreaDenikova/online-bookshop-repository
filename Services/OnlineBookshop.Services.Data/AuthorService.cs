namespace OnlineBookshop.Services.Data;

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using OnlineBookshop.Data.Common.Repositories;
using OnlineBookshop.Data.Models;
using OnlineBookshop.Web.ViewModels.InputModels;

public class AuthorService : IAuthorService
{
    private readonly IDeletableEntityRepository<Author> authorRepository;

    public AuthorService(
        IDeletableEntityRepository<Author> authorRepository)
    {
        this.authorRepository = authorRepository;
    }

    public Author GetAuthor(string authorId) => this.authorRepository.All().Single(a => a.Id == authorId);

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
