namespace NiceOne.Services.Cities
{
    using NiceOne.DTOs.Cities;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICityService
    {
        Task<IEnumerable<CityGetModel>> GetCitiesByCountryAsync(int countryId);
    }
}
