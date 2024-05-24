using System;
using OnlineBookshop.Data.Common.Models;

namespace OnlineBookshop.Data.Models;

public class UserReadSettings : BaseDeletableModel<string>
{
    public UserReadSettings()
    {
        this.Id = Guid.NewGuid().ToString();
    }

    public virtual ApplicationUser User { get; set; }

    public string UserId { get; set; }

    public string Font { get; set; }

    public string FontSize { get; set; }

    public string FontColor { get; set; }

    public string BackgroundColor { get; set; }

    public string TextAlign { get; set; }
}
