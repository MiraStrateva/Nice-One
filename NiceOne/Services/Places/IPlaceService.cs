namespace NiceOne.Services.Places
{
    using NiceOne.Data.Entities;
    using NiceOne.DTOs.Places;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPlaceService
    {
        Task CreateAsync(Place place);
        Task<IEnumerable<PlaceListGetModel>> GetByCategoryAsync(int categoryId);
        Task<IEnumerable<PlaceListGetModel>> AllAysnc();
        Task<IEnumerable<PlaceListGetModel>> GetByUserAsync(string userId);
        Task<PlaceGetModel> GetByIdAsync(int placeId);
        Task<Place> FindAsync(int id);
        Task DeleteAsync(int id);
        Task SaveAsync(Place place);
    }
}
