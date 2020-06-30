namespace NiceOne.Services.Places
{
    using Microsoft.EntityFrameworkCore;

    using NiceOne.Data;
    using NiceOne.Data.Entities;
    using NiceOne.DTOs.Places;

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PlaceService : BaseService<Place>, IPlaceService
    {
        public PlaceService(NiceOneDbContext data) 
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
                .OrderBy(p => p.City.Country.Name)
                .ThenBy(p => p.City.Name)
                .ThenBy(p => p.Name)
                .Select(p => new PlaceListGetModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    CategoryName = p.Category.Name,
                    City = p.City.Name,
                    Country = p.City.Country.Name,
                    Rating = p.Feedbacks.Select(f => f.Rating).Average(),
                    FeedbackCount = p.Feedbacks.Count
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<PlaceListGetModel>> AllAysnc ()
        {
            return await this.Data.Places
                .OrderBy(p => p.Category)
                .ThenBy(p => p.City.Country.Name)
                .ThenBy(p => p.City.Name)
                .ThenBy(p => p.Name)
                .Select(p => new PlaceListGetModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    CategoryName = p.Category.Name,
                    City = p.City.Name,
                    Country = p.City.Country.Name,
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
                    Id  = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name,
                    CityId = p.CityId,
                    CityName = p.City.Name,
                    CountryId = p.City.CountryId,
                    CountryName = p.City.Country.Name,
                    Rating = p.Feedbacks.Select(f => f.Rating).Average(),
                    FeedbackCount = p.Feedbacks.Count(),
                    Feedbacks = p.Feedbacks.Select(f => new PlaceFeedbackGetModel   
                                                {   
                                                    Id = f.Id,
                                                    Text = f.Text,
                                                    Rating = f.Rating, 
                                                    Date = f.Date,
                                                    User = string.Concat(f.User.FirstName, f.User.LastName)
                                                })
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PlaceListGetModel>> GetByUserAsync(string userId)
        {
            return await this.Data.Places
                .Where(p => p.Feedbacks.Any(f => f.UserId == userId))
                .OrderBy(p => p.Category)
                .ThenBy(p => p.City.Country.Name)
                .ThenBy(p => p.City.Name)
                .ThenBy(p => p.Name)
                .Select(p => new PlaceListGetModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    CategoryName = p.Category.Name,
                    City = p.City.Name,
                    Country = p.City.Country.Name,
                    Rating = p.Feedbacks.Select(f => f.Rating).Average(),
                    FeedbackCount = p.Feedbacks.Count
                })
                .ToListAsync();
        }
    }
}
