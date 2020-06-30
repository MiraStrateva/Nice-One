namespace NiceOne.Services.Cities
{
    using NiceOne.Data.Entities;
    using NiceOne.DTOs.Cities;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICityService : IBaseService<City>
    {
        Task<IEnumerable<CityModel>> GetCitiesByCountryAsync(int countryId);
        Task<CityModel> GetByIdAsync(int cityId);
        Task DeleteAsync(int id);
    }
}
