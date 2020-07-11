namespace NiceOne.Place.Services.Places
{
    using NiceOne.Place.Data;
    using NiceOne.Place.Data.Entities;
    using NiceOne.Place.DTOs.Places;
    using NiceOne.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPlaceService : IBaseService<NiceOnePlaceDbContext, Place>
    {
        Task<IEnumerable<PlaceListGetModel>> GetByCategoryAsync(int categoryId);
        Task<IEnumerable<PlaceListGetModel>> AllAysnc();
        Task<IEnumerable<PlaceListGetModel>> GetByUserAsync(string userId);
        Task<IEnumerable<PlaceListGetModel>> SearchPlacesAsync(string search);
        Task<PlaceGetModel> GetByIdAsync(int placeId);
        Task DeleteAsync(int id);
    }
}
