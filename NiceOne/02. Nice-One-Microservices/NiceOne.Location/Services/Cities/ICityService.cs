namespace NiceOne.Location.Services.Cities
{
    using NiceOne.Location.Data;
    using NiceOne.Location.Data.Entities;
    using NiceOne.Location.Models.Cities;
    using NiceOne.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICityService : IBaseService<NiceOneLocationDbContext, City>
    {
        Task<IEnumerable<CityModel>> GetCitiesByCountryAsync(int countryId);
        Task<CityModel> GetByIdAsync(int cityId);
        Task DeleteAsync(int id);
    }
}
