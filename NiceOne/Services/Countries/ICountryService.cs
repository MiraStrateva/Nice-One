namespace NiceOne.Services.Countries
{
    using NiceOne.Data.Entities;
    using NiceOne.DTOs.Countries;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICountryService : IBaseService<Country>
    {
        Task<IEnumerable<CountryModel>> GetAsync();
        Task<CountryModel> GetByIdAsync(int countryId);
        Task DeleteAsync(int id);
    }
}
