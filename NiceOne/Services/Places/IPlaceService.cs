using NiceOne.DTOs.Places;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NiceOne.Services.Places
{
    public interface IPlaceService
    {
        Task CreateAsync(PlaceSetModel placeSetModel);
        Task<IEnumerable<PlaceListGetModel>> GetByCategory(int categoryId);
        Task<IEnumerable<PlaceListGetModel>> GetByUser(string userId);
    }
}
