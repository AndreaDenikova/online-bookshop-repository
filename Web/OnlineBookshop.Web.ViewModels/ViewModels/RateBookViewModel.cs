using OnlineBookshop.Web.ViewModels.InputModels;

namespace OnlineBookshop.Web.ViewModels.ViewModels;

public class RateBookViewModel
{
    public string BookId { get; set; }

    public string Comment { get; set; }

    public int Rate { get; set; }

    public RateBookInputModel InputModel { get; set; }
}
