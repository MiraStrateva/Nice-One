namespace NiceOne.Services.Places
{
    using NiceOne.Data.Entities;
    using NiceOne.DTOs.Places;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPlaceService : IBaseService<Place>
    {
        Task<IEnumerable<PlaceListGetModel>> GetByCategoryAsync(int categoryId);
        Task<IEnumerable<PlaceListGetModel>> AllAysnc();
        Task<IEnumerable<PlaceListGetModel>> GetByUserAsync(string userId);
        Task<IEnumerable<PlaceListGetModel>> SearchPlacesAsync(string search);
        Task<PlaceGetModel> GetByIdAsync(int placeId);
        Task DeleteAsync(int id);
    }
}
