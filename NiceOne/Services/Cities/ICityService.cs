using NiceOne.DTOs.Cities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NiceOne.Services.Cities
{
    public interface ICityService
    {
        Task<IEnumerable<CityGetModel>> GetCitiesByCountryAsync(int countryId);
    }
}
