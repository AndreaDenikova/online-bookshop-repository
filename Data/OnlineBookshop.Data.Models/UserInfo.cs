namespace OnlineBookshop.Data.Models;

using System;
using System.ComponentModel.DataAnnotations;
using OnlineBookshop.Data.Common.Models;

public class UserInfo : BaseDeletableModel<string>
{
    public UserInfo()
    {
        this.Id = Guid.NewGuid().ToString();
    }

    public virtual ApplicationUser User { get; set; }

    public string UserId { get; set; }

    [Required]
    public string ProfilePicture { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string Username { get; set; }

    public string Description { get; set; }
}
