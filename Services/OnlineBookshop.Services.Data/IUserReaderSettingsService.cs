using OnlineBookshop.Web.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookshop.Services.Data
{
    public interface IUserReaderSettingsService
    {
        UserReaderSettingsViewModel GetReaderSettings(string userId);
        Task SaveReaderSettings(UserReaderSettingsViewModel model, string userId);
    }
}
