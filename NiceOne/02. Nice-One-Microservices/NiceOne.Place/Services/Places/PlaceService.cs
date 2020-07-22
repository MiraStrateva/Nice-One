namespace NiceOne.Place.Services.Places
{
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using NiceOne.Messages.Location;
    using NiceOne.Place.Data;
    using NiceOne.Place.Data.Entities;
    using NiceOne.Place.Models.Places;
    using NiceOne.Services;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PlaceService : BaseService<NiceOnePlaceDbContext, Place>, IPlaceService
    {
        public PlaceService(NiceOnePlaceDbContext data)
            : base(data)
        {
        }

        public async Task DeleteAsync(int id)
        {
            var place = new Place { Id = id };
            await this.DeleteAsync(place);
        }

        public async Task<IEnumerable<PlaceListGetModel>> GetByCategoryAsync(int categoryId)
        {
            return await this.Data.Places
                .Where(p => p.CategoryId == categoryId)
                .OrderBy(p => p.CountryName)
                .ThenBy(p => p.CityName)
                .ThenBy(p => p.Name)
                .Select(p => new PlaceListGetModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    CategoryName = p.Category.Name,
                    City = p.CityName,
                    Country = p.CountryName,
                    Rating = p.Feedbacks.Select(f => f.Rating).Average(),
                    FeedbackCount = p.Feedbacks.Count
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<PlaceListGetModel>> AllAysnc()
        {
            return await this.Data.Places
                .OrderBy(p => p.Category)
                .ThenBy(p => p.CountryName)
                .ThenBy(p => p.CityName)
                .ThenBy(p => p.Name)
                .Select(p => new PlaceListGetModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    CategoryName = p.Category.Name,
                    City = p.CityName,
                    Country = p.CountryName,
                    Rating = p.Feedbacks.Select(f => f.Rating).Average(),
                    FeedbackCount = p.Feedbacks.Count
                })
                .ToListAsync();
        }

        public async Task<PlaceGetModel> GetByIdAsync(int placeId)
        {
            return await this.Data.Places
                .Where(p => p.Id == placeId)
                .Select(p => new PlaceGetModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name,
                    CityId = p.CityId,
                    CityName = p.CityName,
                    CountryId = p.CountryId,
                    CountryName = p.CountryName,
                    Rating = p.Feedbacks.Select(f => f.Rating).Average(),
                    FeedbackCount = p.Feedbacks.Count(),
                    Feedbacks = p.Feedbacks.Select(f => new PlaceFeedbackGetModel
                    {
                        Id = f.Id,
                        Text = f.Text,
                        Rating = f.Rating,
                        Date = f.Date,
                        UserId = f.UserId
                    })
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PlaceListGetModel>> GetByUserAsync(string userId)
        {
            return await this.Data.Places
                .Where(p => p.Feedbacks.Any(f => f.UserId == userId))
                .OrderBy(p => p.Category)
                .ThenBy(p => p.CountryName)
                .ThenBy(p => p.CityName)
                .ThenBy(p => p.Name)
                .Select(p => new PlaceListGetModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    CategoryName = p.Category.Name,
                    City = p.CityName,
                    Country = p.CountryName,
                    Rating = p.Feedbacks.Select(f => f.Rating).Average(),
                    FeedbackCount = p.Feedbacks.Count
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<PlaceListGetModel>> SearchPlacesAsync(string search)
        {
            return await this.Data.Places
                .Where(p => p.CityName.Contains(search)
                            || p.CountryName.Contains(search)
                            || p.Category.Name.Contains(search)
                            || p.Name.Contains(search)
                            || p.Description.Contains(search))
                .OrderBy(p => p.Category)
                .ThenBy(p => p.CountryName)
                .ThenBy(p => p.CityName)
                .ThenBy(p => p.Name)
                .Select(p => new PlaceListGetModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    CategoryName = p.Category.Name,
                    City = p.CityName,
                    Country = p.CountryName,
                    Rating = p.Feedbacks.Select(f => f.Rating).Average(),
                    FeedbackCount = p.Feedbacks.Count
                })
                .ToListAsync();
        }

        public async Task UpdateCountryName(CountryUpdatedMessage message)
        {
            string updateCountryQuery = "Update Places Set CountryName = @Name Where CountryId = @Id";
            await Data.Database
                .ExecuteSqlCommandAsync(updateCountryQuery, 
                    new SqlParameter("@Name", message.CountryName), new SqlParameter("@Id", message.CountryId));

            await Data.SaveChangesAsync();
        }

        public async Task UpdateCityName(CityUpdatedMessage message)
        {
            string updateCityQuery = "Update Places Set CityName = @Name Where CityId = @Id";
            await Data.Database
                .ExecuteSqlCommandAsync(updateCityQuery,
                    new SqlParameter("@Name", message.CityName), new SqlParameter("@Id", message.CityId));

            await Data.SaveChangesAsync();
        }
    }
}