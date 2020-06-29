namespace NiceOne.Services.Countries
{
    using NiceOne.DTOs.Countries;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICountryService
    {
        Task<IEnumerable<CountryGetModel>> GetAsync();
    }
}
