using NiceOne.Data;
using NiceOne.Data.Entities;
using NiceOne.DTOs.Places;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NiceOne.Services.Places
{
    public class PlaceService : BaseService<Place>, IPlaceService
    {
        public PlaceService(NiceOneDbContext data) 
            : base(data)
        {
        }

        public async Task CreateAsync(PlaceSetModel placeSetModel)
        {
            var place = new Place()
            {
                CategoryId = placeSetModel.CategoryId,
                Name = placeSetModel.Name,
                Description = placeSetModel.Description,
                CityId = placeSetModel.CityId
            };
            this.Data.Places.Add(place);
            await this.Data.SaveChangesAsync();
        }

        public async Task<IEnumerable<PlaceListGetModel>> GetByCategory(int categoryId)
        {
            return this.Data.Places
                .Where(p => p.CategoryId == categoryId)
                .Select(p => new PlaceListGetModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    CategoryName = p.Category.Name,
                    City = p.City.Name,
                    Country = p.City.Country.Name,
                    Rating = 0, //p.Feedbacks.Average<(f => f.Rating),
                    FeedbackCount = p.Feedbacks.Count,
                    ImageUrl = ""
                });
        }

        public async Task<IEnumerable<PlaceListGetModel>> GetByUser(string userId)
        {
            return this.Data.Places
                .Where(p => p.Feedbacks.Any(f => f.UserId == userId))
                .Select(p => new PlaceListGetModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    CategoryName = p.Category.Name,
                    City = p.City.Name,
                    Country = p.City.Country.Name,
                    Rating = 0, //p.Feedbacks.Average<(f => f.Rating),
                    FeedbackCount = p.Feedbacks.Count,
                    ImageUrl = ""
                });
        }


    }
}
