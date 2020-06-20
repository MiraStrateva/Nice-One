using NiceOne.DTOs.Countries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NiceOne.Services.Countries
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryGetModel>> GetAsync();
    }
}
