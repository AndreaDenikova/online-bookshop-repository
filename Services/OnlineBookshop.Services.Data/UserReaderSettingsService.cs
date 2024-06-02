using OnlineBookshop.Data.Common.Repositories;
using OnlineBookshop.Data.Models;
using OnlineBookshop.Web.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookshop.Services.Data
{
    public class UserReaderSettingsService : IUserReaderSettingsService
    {
        private readonly IDeletableEntityRepository<UserReadSettings> userReadSettingsRepository;

        public UserReaderSettingsService(IDeletableEntityRepository<UserReadSettings> userReadSettingsRepository)
        {
            this.userReadSettingsRepository = userReadSettingsRepository;
        }

        public UserReaderSettingsViewModel GetReaderSettings(string userId)
        {
            return this.userReadSettingsRepository
                .All()
                .Where(x => x.UserId == userId)
                .Select(x => new UserReaderSettingsViewModel
                {
                    BackgroundColor = x.BackgroundColor,
                    Font = x.Font,
                    FontColor = x.FontColor,
                    FontSize = x.FontSize,
                    TextAlign = x.TextAlign,
                })
                .FirstOrDefault();
        }

        public async Task SaveReaderSettings(UserReaderSettingsViewModel model, string userId)
        {
            var userReaderSettings = this.userReadSettingsRepository
                .All()
                .FirstOrDefault(x => x.UserId == userId);

            if (userReaderSettings == null)
            {
                userReaderSettings = new UserReadSettings
                {
                    UserId = userId,
                    BackgroundColor = model.BackgroundColor,
                    Font = model.Font,
                    FontColor = model.FontColor,
                    FontSize = model.FontSize,
                    TextAlign = model.TextAlign,
                };

                await this.userReadSettingsRepository.AddAsync(userReaderSettings);
            }
            else
            {
                userReaderSettings.BackgroundColor = model.BackgroundColor;
                userReaderSettings.Font = model.Font;
                userReaderSettings.FontColor = model.FontColor;
                userReaderSettings.FontSize = model.FontSize;
                userReaderSettings.TextAlign = model.TextAlign;
            }

            await this.userReadSettingsRepository.SaveChangesAsync();
        }
    }
}
