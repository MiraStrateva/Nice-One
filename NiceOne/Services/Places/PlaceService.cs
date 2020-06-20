using NiceOne.Data;
using NiceOne.Data.Entities;
using NiceOne.DTOs.Places;
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
    }
}
